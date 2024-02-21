﻿using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using System.Net;

namespace ABMS_backend.Services
{
    public class CmbAccountManagementService : ICmbAccountManagementRepository
    { 
        private readonly abmsContext _abmsContext;

        public CmbAccountManagementService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
        }

<<<<<<< HEAD
        ResponseData<string> ICmbAccountManagementRepository.createCmbAccount(AccountDTO dto)
=======
        ResponseData<string> ICmbAccountManagementRepository.createCmbAccount(AccountForInsertDTO dto)
>>>>>>> ae8801e6f333eda5eeb0e6347c14a65027ee5e0b
        {
            Account account = new Account();
            account.ApartmentId = dto.apartmentId;
            account.PhoneNumber = dto.phone;
            account.PasswordSalt = dto.pwd_salt;
            account.PasswordHash = dto.pwd_hash;
            account.Email = dto.email;
            account.FullName = dto.full_name;
            account.Avatar = dto.avatar;
            account.Id = Guid.NewGuid().ToString();
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

        ResponseData<string> ICmbAccountManagementRepository.deleteCmbAccount(string id)
        {
            throw new NotImplementedException();
        }

        List<ResponseData<Account>> ICmbAccountManagementRepository.getCmbAccount(AccountForSearchDTO dto)
        {
            throw new NotImplementedException();
        }

        ResponseData<Account> ICmbAccountManagementRepository.getCmbAccountById(string id)
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        ResponseData<string> ICmbAccountManagementRepository.updateCmbAccount(string id, AccountDTO dto)
=======
        ResponseData<string> ICmbAccountManagementRepository.updateCmbAccount(string id, AccountForInsertDTO dto)
>>>>>>> ae8801e6f333eda5eeb0e6347c14a65027ee5e0b
        {
            throw new NotImplementedException();
        }
    }
}
