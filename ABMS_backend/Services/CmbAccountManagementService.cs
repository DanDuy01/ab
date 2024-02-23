using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using System.Net;
using AutoMapper;
using ABMS_backend.Utils.Exceptions;
using System.Collections.Generic;

namespace ABMS_backend.Services
{
    public class CmbAccountManagementService : ICmbAccountManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        public CmbAccountManagementService(abmsContext abmsContext, IMapper mapper)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
        }

        ResponseData<string> ICmbAccountManagementRepository.createCmbAccount(AccountForInsertDTO dto)
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
                Account account = new Account();
                account.Id = Guid.NewGuid().ToString();
                account.ApartmentId = dto.apartmentId;
                account.PhoneNumber = dto.phone;
                account.PasswordSalt = dto.pwd_salt;
                account.PasswordHash = dto.pwd_hash;
                account.Email = dto.email;
                account.FullName = dto.full_name;
                account.Avatar = dto.avatar;
                account.CreateUser = "admin";
                account.CreateTime = DateTime.Now;
                account.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Accounts.Add(account);
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
                    ErrMsg = "Created failed why " + ex.Message
                };
            }

        }

        ResponseData<string> ICmbAccountManagementRepository.updateCmbAccount(string id, AccountForInsertDTO dto)
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
                account.ApartmentId = dto.apartmentId;
                account.PhoneNumber = dto.phone;
                account.PasswordSalt = dto.pwd_salt;
                account.PasswordHash = dto.pwd_hash;
                account.Email = dto.email;
                account.FullName = dto.full_name;
                account.Avatar = dto.avatar;
                account.ModifyUser = "admin";
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

        ResponseData<string> ICmbAccountManagementRepository.deleteCmbAccount(string id)
        {
            try
            {
                Account account = _abmsContext.Accounts.Find(id);
                if (account == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                account.Status = (int)Constants.STATUS.IN_ACTIVE;
                account.ModifyUser = "admin";
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
                    ErrMsg = "Deleted failed why " + ex.Message
                };
            }
        }

        ResponseData<List<Account>> ICmbAccountManagementRepository.getCmbAccount(AccountForSearchDTO dto)
        {
            var list = _abmsContext.Accounts.
                Where(x => (dto.searchMessage == null || x.PhoneNumber.Contains(dto.searchMessage.ToLower()) 
                || x.Email.ToLower().Contains(dto.searchMessage.ToLower()) 
                || x.FullName.ToLower().Contains(dto.searchMessage.ToLower()))
                && (dto.apartmentId == null || x.ApartmentId.Equals(dto.apartmentId.ToLower()))
                && (dto.status == null || x.Status == dto.status)).ToList();
            return new ResponseData<List<Account>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        ResponseData<Account> ICmbAccountManagementRepository.getCmbAccountById(string id)
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


    }
}
