using ABMS_backend.DTO;
using ABMS_backend.DTO.UtilityDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IUtilityManagementRepository
    {
        ResponseData<string> createUtility(UtilityForInsertDTO dto);

        ResponseData<string> updateUtility(string id, UtilityForInsertDTO dto);

        ResponseData<string> deleteUtility(string id);

        ResponseData<List<Utility>> getAllUtility(UtilityForSearch dto);

        ResponseData<Utility> getUtilityById(string id);

        ResponseData<string> restore(List<string> id);

        ResponseData<string> remove(List<string> id);

        ResponseData<bool> CheckUtilityDetailsHaveSchedules(string utilityId);
        ResponseData<string> createUtilityDetail(UtilityDetailDTO dto);

        ResponseData<string> updateUtilityDetail(string id, string name);

        ResponseData<string> deleteUtilityDetail(string id);

        ResponseData<List<UtiliityDetail>> getUtilityDetail(String? utilityId);

        ResponseData<UtiliityDetail> getUtilityDetailById(string id);
    }
}
