using ABMS_backend.DTO;
using ABMS_backend.DTO.ElevatorDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IElevatorRepository
    {
        ResponseData<string> createElevator(ElevatorForInsertDTO dto);

        ResponseData<string> updateElevator(string id, ElevatorForEditDTO dto);

        ResponseData<string> manageElevator(string id, int status);

        ResponseData<string> deleteElevator(string id);

        ResponseData<List<Elevator>> getElevator(ElevatorForSearchDTO dto);

        ResponseData<Elevator> getElevatorById(string id);
    }
}
