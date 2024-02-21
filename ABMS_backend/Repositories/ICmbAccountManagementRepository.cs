using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ICmbAccountManagementRepository
    {
        ResponseData<string> createCmbAccount(AccountDTO dto);

        ResponseData<string> updateCmbAccount(string id, AccountDTO dto);

        ResponseData<string> deleteCmbAccount(string id);

        List<ResponseData<Account>> getCmbAccount(CmbAccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
    }
}
