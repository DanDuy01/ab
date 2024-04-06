using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.DTO.FundDTO;

namespace ABMS_backend.Repositories
{
    public interface IFundManagementRepository
    {
        ResponseData<string> createFund(FundForInsertDTO dto);
        ResponseData<string> deleteFund(string id);
        ResponseData<string> updateFund(string id, FundForInsertDTO dto);
        ResponseData<List<Fund>> getAllFund(FundForSearchDTO dto);
        ResponseData<Fund> getFundById(string id);
        byte[] ExportData(string buildingId);
    }
}
