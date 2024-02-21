using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ICmbAccountManagementRepository
    {
<<<<<<< HEAD
        ResponseData<string> createCmbAccount(AccountDTO dto);

        ResponseData<string> updateCmbAccount(string id, AccountDTO dto);
=======
        ResponseData<string> createCmbAccount(AccountForInsertDTO dto);

        ResponseData<string> updateCmbAccount(string id, AccountForInsertDTO dto);
>>>>>>> ae8801e6f333eda5eeb0e6347c14a65027ee5e0b

        ResponseData<string> deleteCmbAccount(string id);

        List<ResponseData<Account>> getCmbAccount(AccountForSearchDTO dto);

        ResponseData<Account> getCmbAccountById(string id);
    }
}
