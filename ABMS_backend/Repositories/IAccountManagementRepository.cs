using ABMS_backend.DTO;
using ABMS_backend.DTO.AccountDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IAccountManagementRepository
    {
        ResponseData<string> updateCmbAccount(string id, AccountForUpdateDTO dto);

        ResponseData<string> deleteCmbAccount(string id);
        ResponseData<string> activeAccount(string id, int status);

        ResponseData<List<Account>> getCmbAccount(AccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
        ResponseData<string> DeleteAccountAndRelatedData(string accountId);
    }
}