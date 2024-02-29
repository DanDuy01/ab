
using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IRoomInformationRepository
    {
        ResponseData<string> createRoomInformation(RoomForInsertDTO dto);
        ResponseData<string> updateRoomInformation(string id,RoomForInsertDTO dto);
        ResponseData<string> deleteRoomInformation(string id);
        ResponseData<Room> getRoomInformationById(string id);
        ResponseData<List<Room>> getRoomInformation(RoomForSearchDTO dto);
    }
}
