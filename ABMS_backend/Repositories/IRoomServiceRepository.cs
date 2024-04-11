using ABMS_backend.DTO.RoomServiceDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IRoomServiceRepository
    {
        ResponseData<string> createRoomService(RoomServiceForInsertDTO dto);

        ResponseData<string> deleteRoomService(String id);

        ResponseData<String> deleteDraftRoomService(List<String> idList);

        ResponseData<List<RoomService>> getRoomService(RoomServiceForSearchDTO dto);

        ResponseData<RoomService> getRoomServiceById(String id);
        ResponseData<string> DeleteRoomServicesInBuilding(string buildingId);
        ResponseData<bool> CheckUnassignedRoomServicesInBuilding(string buildingId);
        ResponseData<string> deleteByFeeRoomId(string feeId, string roomId);
    }
}
