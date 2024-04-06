using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.DTO.ServiceChargeDTO;

namespace ABMS_backend.Repositories
{
    public interface IServiceChargeRepository
    {
        ResponseData<string> createServiceCharge(ServiceChargeForInsertDTO dto);

        ResponseData<string> updateServiceCharge(string id, string? description, int? status);

        ResponseData<string> deleteServiceCharge(string id);

        ResponseData<List<ServiceChargeListResponseDTO>> getServiceCharge(ServiceChargeForSearchDTO dto);

        ResponseData<ServiceCharge> getServiceChargeById(string id);

        ResponseData<List<ServiceChargeResponseDTO>> getTotal(String room_id, int? status);

        byte[] ExportData(string buildingId);
    }
}
