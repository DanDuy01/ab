using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IReservationManagement
    {
        ResponseData<string> createReservation(ReservationForInsertDTO dto);

        ResponseData<string> updateReservation(string id, ReservationForInsertDTO dto);

        ResponseData<string> deleteReservation(string id);

        ResponseData<List<Utility>> getAllReservation(UtilityForSearch dto);

        ResponseData<List<UtilityForInsertDTO>> getReservation(UtilityForSearch dto);

        ResponseData<Utility> getReservationById(string id);
    }
}
