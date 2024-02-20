using ABMS_backend.DTO;
using ABMS_backend.Models;
namespace ABMS_backend.Services
{
    public interface ICmbAccountManagementService
    {
        ResponseData<String> createCmbAccount(CmbAccountForInsertDTO dto);

        ResponseData<String> updateCmbAccount(String id, CmbAccountForInsertDTO dto);

        ResponseData<String> deleteCmbAccount(String id);

        List<ResponseData<Account>> getCmbAccount(CmbAccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(String id);
    }
}
