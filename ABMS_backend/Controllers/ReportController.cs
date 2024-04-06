using ABMS_backend.DTO;
using ABMS_backend.DTO.ReportDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ReportController : Controller
    {
        private IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("report/utility-report/{buildingId}")]
        public ResponseData<List<UtilityReservationCountDTO>> UtilityReport(String buildingId)
        {
            ResponseData<List<UtilityReservationCountDTO>> response = _repository.GetUtilityReservationCounts(buildingId);
            return response;
        }

        [HttpGet("report/building-report/{buildingId}")]
        public ResponseData<ReportDTO> BuildingReport(String buildingId)
        {
            ResponseData<ReportDTO> response = _repository.buildingReport(buildingId);
            return response;
        }
        
    }
}
