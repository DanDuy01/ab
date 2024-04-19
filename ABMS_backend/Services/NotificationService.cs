using ABMS_backend.DTO;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class NotificationService : INotificationRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHubContext<NotificationHub> _hubContext;

        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IHubContext<NotificationHub> hubContext)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
             _hubContext = hubContext;
        }

        public async Task<ResponseData<string>> createNotificationForReceptionistAsync(NotificationForRecepionistDTO dto)
        {
            string error = dto.Validate();

            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            try
            {
                Notification notification = new Notification();
                notification.Id = Guid.NewGuid().ToString();
                notification.Title = dto.title;
                notification.Content = dto.content;
                notification.ServiceId = dto.serviceId;
                notification.CreateTime = DateTime.Now;
                notification.BuildingId = dto.buildingId;
                notification.Type = 2;
                _abmsContext.Notifications.Add(notification);
                _abmsContext.SaveChanges();

                var targetAccounts = _abmsContext.Accounts.Where(a => a.Role == 2 && a.BuildingId == dto.buildingId).ToList();

                foreach (var account in targetAccounts)
                {
                    var NotificationAccount = new NotificationAccount
                    {
                        Id = Guid.NewGuid().ToString(),
                        AccountId = account.Id,
                        NotificationId = notification.Id,
                        IsRead = 0
                    };
                    _abmsContext.NotificationAccounts.Add(NotificationAccount);
                }

                _abmsContext.SaveChanges();
                await _hubContext.Clients.Group(dto.buildingId.ToString()).SendAsync("ReceiveNotification", notification);
                return new ResponseData<string>
                {
                    Data = notification.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }


        public ResponseData<string> createNotificationForResident(NotificationForResidentDTO dto)
        {
            string error = dto.Validate();

            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }

            try
            {
                Notification notification = new Notification
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = dto.title,
                    Content = dto.content,
                    BuildingId = dto.buildingId,
                    CreateTime = DateTime.Now,
                    Type = 1
                };

                _abmsContext.Notifications.Add(notification);

                if (dto.content == "Bill")
                {
                    var rooms = _abmsContext.Rooms.Where(r => r.BuildingId == dto.buildingId).ToList();
                    foreach (var room in rooms)
                    {
                        var account = _abmsContext.Accounts.FirstOrDefault(a => a.Id == room.AccountId);
                        if (account != null)
                        {
                            var notificationAccount = new NotificationAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountId = account.Id,
                                NotificationId = notification.Id,
                                IsRead = 0
                            };
                            _abmsContext.NotificationAccounts.Add(notificationAccount);
                        }
                    }
                }
                else
                {
                    var room = _abmsContext.Rooms.FirstOrDefault(r => r.Id == dto.roomId && r.BuildingId == dto.buildingId);
                    if (room == null)
                    {
                        return new ResponseData<string>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            ErrMsg = "Room not found."
                        };
                    }

                    var targetAccount = _abmsContext.Accounts.FirstOrDefault(a => a.Id == room.AccountId);
                    if (targetAccount != null)
                    {
                        var notificationAccount = new NotificationAccount
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = targetAccount.Id,
                            NotificationId = notification.Id,
                            IsRead = 0
                        };
                        _abmsContext.NotificationAccounts.Add(notificationAccount);
                    }
                    else
                    {
                        return new ResponseData<string>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            ErrMsg = "Account not found."
                        };
                    }
                }

                _abmsContext.SaveChanges();

                return new ResponseData<string>
                {
                    Data = notification.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Creation failed: " + ex.Message
                };
            }
        }


        public ResponseData<List<Notification>> getNotification(string? buildingId)
        {
            var list = _abmsContext.Notifications.Where(x=>
            (buildingId == null || x.BuildingId == buildingId)).ToList();
            return new ResponseData<List<Notification>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<string> updateNotification(string id)
        {
            Notification notification = _abmsContext.Notifications.Find(id);
            if (notification == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            _abmsContext.Notifications.Update(notification);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public IEnumerable<NotificationAccountDTO> GetNotifications(string accountId, int skip, int take)
        {
            var notifications = _abmsContext.NotificationAccounts
        .Where(ap => ap.AccountId == accountId)
        .OrderByDescending(ap => ap.Notification.CreateTime)
        .Select(ap => new NotificationAccountDTO
        {
            Notification = ap.Notification,
            IsRead = ap.IsRead == 1 // Assuming IsRead is stored as an int (1 for true, 0 for false)
        })
        .Skip(skip)
        .Take(take)
        .ToList();

            return notifications;
        }

        public void MarkNotificationsAsRead(string accountId)
        {
            var unreadNotifications = _abmsContext.NotificationAccounts
                .Where(ap => ap.AccountId == accountId && ap.IsRead == 0)
                .ToList();

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = 1;
            }

            _abmsContext.SaveChanges();
        }
        public async Task<ResponseData<string>> DeleteNotificationsByServiceIdAsync(string serviceId)
        {
            var notificationsToDelete = _abmsContext.Notifications.Where(n => n.ServiceId == serviceId).ToList();

            if (!notificationsToDelete.Any())
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrMsg = "No notifications found with the provided service ID."
                };
            }

            foreach (var notification in notificationsToDelete)
            {
                var relatedAccounts = _abmsContext.NotificationAccounts.Where(na => na.NotificationId == notification.Id).ToList();
                _abmsContext.NotificationAccounts.RemoveRange(relatedAccounts);
                _abmsContext.Notifications.Remove(notification);
            }

            await _abmsContext.SaveChangesAsync();

            return new ResponseData<string>
            {
                Data = "Deleted all notifications with service ID: " + serviceId,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

    }
}
