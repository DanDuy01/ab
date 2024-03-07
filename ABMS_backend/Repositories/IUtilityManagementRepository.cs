using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IUtilityManagementRepository
    {
        ResponseData<string> createUtility(UtilityForInsertDTO dto);

        ResponseData<string> createUtilityDetail(UtilityDetailDTO dto);

        ResponseData<string> updateUtility(string id, UtilityForInsertDTO dto);

        ResponseData<string> deleteUtility(string id);

        ResponseData<List<Utility>> getAllUtility(UtilityForSearch dto);

        ResponseData<List<UtiliityDetail>> getUtilityDetail();

        ResponseData<Utility> getUtilityById(string id);
    }
}
