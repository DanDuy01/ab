using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.Net;

namespace ABMS_backend.Services
{
    public class ReceptionAccountManagerService : IReceptionistAccountRepository
    {
        private readonly abmsContext _abmsContext;

        public ReceptionAccountManagerService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
        }
        public ResponseData<string> createReceptionAccount(AccountForInsertDTO dto)
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
                Account account = new Account();
                account.Id = Guid.NewGuid().ToString();
                account.ApartmentId = dto.apartmentId;
                account.PhoneNumber = dto.phone;
                account.PasswordSalt = dto.pwd_salt;
                account.PasswordHash = dto.pwd_hash;
                account.Email = dto.email;
                account.FullName = dto.full_name;
                account.Avatar = dto.avatar;
                account.CreateUser = "reception";
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

        public ResponseData<string> deleteReceptionAccount(string id)
        {
            try
            {
                Account account = _abmsContext.Accounts.Find(id);
                if (account == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                account.Status = (int)Constants.STATUS.IN_ACTIVE;
                account.ModifyUser = "reception";
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

        public ResponseData<List<Account>> getReceptionAccount(AccountForSearchDTO dto)
        {
            var list = _abmsContext.Accounts.
                Where(x => (dto.apartmentId == null || x.ApartmentId.Contains(dto.apartmentId.ToLower()))
                && (dto.phone == null || x.PhoneNumber.Contains(dto.phone.ToLower()))
                && (dto.email == null || x.Email.Contains(dto.email.ToLower()))
                && (dto.full_name == null || x.FullName.Contains(dto.full_name.ToLower()))
                && (dto.status == null || x.Status == dto.status)).ToList();
            return new ResponseData<List<Account>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Account> getReceptionAccountById(string id)
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

        public ResponseData<string> updateReceptionAccount(string id, AccountForInsertDTO dto)
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
                account.ModifyUser = "reception";
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
