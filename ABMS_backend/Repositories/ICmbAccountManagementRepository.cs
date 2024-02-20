using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ICmbAccountManagementRepository
    {
        ResponseData<string> createCmbAccount(CmbAccountForInsertDTO dto);

        ResponseData<string> updateCmbAccount(string id, CmbAccountForInsertDTO dto);

        ResponseData<string> deleteCmbAccount(string id);

        List<ResponseData<Account>> getCmbAccount(CmbAccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
    }
}
