using ABMS_backend.DTO;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class NotificationService : INotificationRepository
    {
        private readonly abmsContext _abmsContext;

        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createNotificationForReceptionist(NotificationForRecepionistDTO dto)
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
                List<string> list = new List<string>();
                var accounts = _abmsContext.Accounts.Where(x => x.BuildingId == dto.building_id && x.Role == 2);
                foreach (var account in accounts)
                {
                    Notification notification = new Notification();
                    notification.Id = Guid.NewGuid().ToString();
                    notification.AccountId = account.Id;
                    notification.ServiceId = dto.service_id;
                    notification.ServiceName = dto.service_name;
                    notification.Title = dto.title;
                    notification.Content = dto.content;
                    notification.IsRead = false;
                    _abmsContext.Notifications.Add(notification);
                    _abmsContext.SaveChanges();
                    list.Add(notification.Id);
                }
                string jsonData = JsonConvert.SerializeObject(list);
                return new ResponseData<string>
                {
                    Data = jsonData,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description,
                    Count = list.Count
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
                Notification notification = new Notification();
                notification.Id = Guid.NewGuid().ToString();
                notification.AccountId = dto.account_id;
                notification.ServiceId = dto.service_id;
                notification.ServiceName = dto.service_name;
                notification.Title = dto.title;
                notification.Content = dto.content;
                notification.IsRead = false;
                _abmsContext.Notifications.Add(notification);
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
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Notification>> getNotification(string account_id, bool? isRead)
        {
            var list = _abmsContext.Notifications.Where(x => x.AccountId == account_id &&
            (isRead == null || x.IsRead == isRead)).ToList();
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
            notification.IsRead = true;
            _abmsContext.Notifications.Update(notification);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
