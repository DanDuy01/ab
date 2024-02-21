using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Services
{
    public class ReceptionAccountManagerService : IReceptionistAccountRepository
    {
        private readonly abmsContext _abmsContext;

        public ReceptionAccountManagerService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
        }
        public ResponseData<string> createReceptionAccount(AccountDTO dto)
        {
            throw new NotImplementedException();
        }

        public ResponseData<string> deleteReceptionAccount(string id)
        {
            throw new NotImplementedException();
        }

        public List<ResponseData<Account>> getReceptionAccount(CmbAccountForSearchDTO dto)
        {
            throw new NotImplementedException();
        }

        public ResponseData<Account> getReceptionAccountById(string id)
        {
            throw new NotImplementedException();
        }

        public ResponseData<string> updateReceptionAccount(string id, AccountDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
