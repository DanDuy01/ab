using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ICmbAccountManagementRepository
    {
        ResponseData<string> createCmbAccount(AccountForInsertDTO dto);

        ResponseData<string> updateCmbAccount(string id, AccountForInsertDTO dto);

        ResponseData<string> deleteCmbAccount(string id);

        List<ResponseData<Account>> getCmbAccount(AccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
    }
}
