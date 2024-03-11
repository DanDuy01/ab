using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IParkingCardRepository
    {
        ResponseData<string> createParkingCard(ParkingCardForInsertDTO dto);

        ResponseData<string> updateParkingCard(string id, ParkingCardForEditDTO dto);

        ResponseData<string> deleteParkingCard(string id);

        ResponseData<List<ParkingCard>> getParkingCard(ParkingCardForSearchDTO dto);

        ResponseData<ParkingCard> getParkingCardById(string id);
    }
}
