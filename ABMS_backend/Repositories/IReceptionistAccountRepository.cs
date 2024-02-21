using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Services
{
    public interface IReceptionistAccountRepository
    {
        ResponseData<string> createReceptionAccount(AccountDTO dto);

        ResponseData<string> updateReceptionAccount(string id, AccountDTO dto);

        ResponseData<string> deleteReceptionAccount(string id);

        List<ResponseData<Account>> getReceptionAccount(CmbAccountForSearchDTO dto);

        ResponseData<Account> getReceptionAccountById(string id);
    }
}
