using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using System.Net;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.DTO.AccountDTO;
using Microsoft.EntityFrameworkCore;

namespace ABMS_backend.Services
{
    public class AccountManagementService : IAccountManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountManagementService(abmsContext abmsContext, IHttpContextAccessor httpContext)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContext;
        }

        ResponseData<string> IAccountManagementRepository.updateCmbAccount(string id, AccountForUpdateDTO dto)
        {
            //validate
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
                Account account = _abmsContext.Accounts.Find(id);
                if (account == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                Account account1 = _abmsContext.Accounts.FirstOrDefault(x => x.PhoneNumber == dto.phone || x.Email == dto.email);
                if (account1 != null && account1 != account)
                {
                    throw new CustomException(ErrorApp.ACCOUNT_EXISTED);
                }
                account.BuildingId = dto.building_id;
                account.PhoneNumber = dto.phone;
                account.Email = dto.email;
                account.FullName = string.IsNullOrWhiteSpace(dto.full_name) ? account.FullName : dto.full_name;
                account.Role = dto.role;
                account.UserName = string.IsNullOrWhiteSpace(dto.user_name) ? account.UserName : dto.user_name;
                account.Avatar = string.IsNullOrWhiteSpace(dto.avatar) ? account.Avatar : dto.avatar;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                account.ModifyUser = getUser;
                account.ModifyTime = DateTime.Now;
                _abmsContext.Accounts.Update(account);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = account.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Updated failed why " + ex.Message
                };
            }
        }

        ResponseData<string> IAccountManagementRepository.deleteCmbAccount(string id)
        {
            try
            {
                //xoa account
                Account account = _abmsContext.Accounts.Find(id);
                if (account == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }               
                account.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                account.ModifyUser = getUser;
                account.ModifyTime = DateTime.Now;
                _abmsContext.Accounts.Update(account);               
                var residents = _abmsContext.Residents.Where(x => x.Room.AccountId == id).ToList();
                foreach (var r in residents)
                {
                    r.Status = 0;
                    if(r.IsHouseholder == true)
                    {
                        r.IsHouseholder = false;
                    }
                    r.ModifyUser = getUser;
                    r.ModifyTime = DateTime.Now;
                }
                _abmsContext.Residents.UpdateRange(residents);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = account.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Deleted failed why " + ex.Message
                };
            }
        }

        ResponseData<List<Account>> IAccountManagementRepository.getCmbAccount(AccountForSearchDTO dto)
        {
            var list = _abmsContext.Accounts.
                Where(x => (dto.searchMessage == null || x.PhoneNumber.Contains(dto.searchMessage.ToLower()) 
                || x.Email.ToLower().Contains(dto.searchMessage.ToLower()) 
                || x.FullName.ToLower().Contains(dto.searchMessage.ToLower()))
                && (dto.buildingId == null || x.BuildingId.Equals(dto.buildingId))
                && (dto.role == null || x.Role.Equals(dto.role))
                && (dto.status == null || x.Status == dto.status)).ToList();
            return new ResponseData<List<Account>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }



        ResponseData<Account> IAccountManagementRepository.getCmbAccountById(string id)
        {
            Account account = _abmsContext.Accounts.Find(id);
            if (account == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Account>
            {
                Data = account,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
        public ResponseData<string> DeleteAccountAndRelatedData(string accountId)
        {
            var strategy = _abmsContext.Database.CreateExecutionStrategy();
            try
            {
                strategy.Execute(() =>
                {
                    using (var transaction = _abmsContext.Database.BeginTransaction())
                    {
                        Account accountToDelete = _abmsContext.Accounts.Include("Rooms").FirstOrDefault(a => a.Id == accountId);
                        if (accountToDelete == null)
                        {
                            throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                        }

                        // Delete related entities in Rooms and their connected entities
                        foreach (var room in accountToDelete.Rooms)
                        {
                            _abmsContext.Constructions.RemoveRange(room.Constructions);
                            _abmsContext.Elevators.RemoveRange(room.Elevators);
                            _abmsContext.Feedbacks.RemoveRange(room.Feedbacks);
                            _abmsContext.Residents.RemoveRange(room.Residents);
                            _abmsContext.RoomServices.RemoveRange(room.RoomServices);
                            _abmsContext.ServiceCharges.RemoveRange(room.ServiceCharges);
                            _abmsContext.UtilitySchedules.RemoveRange(room.UtilitySchedules);
                            _abmsContext.Visitors.RemoveRange(room.Visitors);
                        }

                        _abmsContext.Rooms.RemoveRange(accountToDelete.Rooms);
                        _abmsContext.NotificationAccounts.RemoveRange(accountToDelete.NotificationAccounts);
                        _abmsContext.Otps.RemoveRange(accountToDelete.Otps);
                        _abmsContext.Accounts.Remove(accountToDelete);

                        _abmsContext.SaveChanges();
                        transaction.Commit();
                    }
                });

                return new ResponseData<string>
                {
                    Data = accountId,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Deletion failed due to: " + ex.Message
                };
            }
        }
        public ResponseData<string> activeAccount(string id, int status)
        {
            try
            {
                Account account = _abmsContext.Accounts.Find(id);
                if (account == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                account.Status = status;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                account.ModifyUser = getUser;
                account.ModifyTime = DateTime.Now;
                _abmsContext.Accounts.Update(account);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = account.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Updated failed why " + ex.Message
                };
            }
        }
    }
}
