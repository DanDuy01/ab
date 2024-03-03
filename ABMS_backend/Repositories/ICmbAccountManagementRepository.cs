using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ICmbAccountManagementRepository
    {
        ResponseData<string> updateCmbAccount(string id, AccountForInsertDTO dto);

        ResponseData<string> deleteCmbAccount(string id);

        ResponseData<List<Account>> getCmbAccount(AccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
    }
}