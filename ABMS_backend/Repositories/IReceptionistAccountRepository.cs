using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Services
{
    public interface IReceptionistAccountRepository
    {
        ResponseData<string> createReceptionAccount(AccountForInsertDTO dto);

        ResponseData<string> updateReceptionAccount(string id, AccountForInsertDTO dto);

        ResponseData<string> deleteReceptionAccount(string id);

        ResponseData<List<Account>> getReceptionAccount (AccountForSearchDTO dto);

        ResponseData<Account> getReceptionAccountById(string id);
    }
}
