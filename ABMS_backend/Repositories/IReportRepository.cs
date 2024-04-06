using ABMS_backend.DTO;
using ABMS_backend.DTO.ReportDTO;

namespace ABMS_backend.Repositories
{
    public interface IReportRepository
    {
        ResponseData<ReportDTO> buildingReport(string buildingId);
        public ResponseData<List<UtilityReservationCountDTO>> GetUtilityReservationCounts(string buildingId);

    }
}
