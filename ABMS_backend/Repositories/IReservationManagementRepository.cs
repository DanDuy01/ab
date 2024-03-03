using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IReservationManagementRepository
    {
        ResponseData<string> createReservation(ReservationForInsertDTO dto);

        ResponseData<string> updateReservation(String id, ReservationForInsertDTO dto);

        ResponseData<string> deleteReservation(string id);

        ResponseData<string> manageReservation(string id, int status);

        ResponseData<List<ReservationResponseDTO>> getReservation(ResevationForResidentSearchDTO dto);

        ResponseData<ReservationResponseDTO> getReservationById(String id);
    }
}
