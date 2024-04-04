using ABMS_backend.DTO;
using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IFeeManagementRepository
    {
        ResponseData<string> createFee(FeeForInsertDTO dto);
        ResponseData<string> deleteFee(string id);
        ResponseData<string> updateFee(string id, FeeForInsertDTO dto);
        ResponseData<List<Fee>> getAllFee(FeeForSearchDTO dto);
        ResponseData<Fee> getFeeById(string id);
        public ResponseData<bool> CheckSpecificFeesExistence(string buildingId);
        public ResponseData<string> AssignFeesToAllRoomsInBuilding(string buildingId);
        ResponseData<List<string>> CheckRoomsMissingFees(string buildingId);
    }
}
