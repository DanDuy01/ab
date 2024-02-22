using ABMS_backend.DTO;
using ABMS_backend.Models;
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
            Account account = new Account();
            account.ApartmentId = dto.apartmentId;
            account.PhoneNumber = dto.phone;
            account.PasswordSalt = dto.pwd_salt;
            account.PasswordHash = dto.pwd_hash;
            account.Email = dto.email;
            account.FullName = dto.full_name;
            account.Avatar = dto.avatar;
            account.Id = Guid.NewGuid().ToString();
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

        public ResponseData<string> deleteReceptionAccount(string id)
        {
            throw new NotImplementedException();
        }

        public List<ResponseData<Account>> getReceptionAccount(ReceptionAccountManagerService dto)
        {
            throw new NotImplementedException();
        }

        public ResponseData<Account> getReceptionAccountById(string id)
        {
            throw new NotImplementedException();
        }

        public ResponseData<string> updateReceptionAccount(string id, AccountForInsertDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
