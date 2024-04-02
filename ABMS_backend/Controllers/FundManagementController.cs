using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.FundDTO;
using ABMS_backend.Models;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class FundManagementController : ControllerBase
    {
        private IFundManagementRepository _repository;

        public FundManagementController(IFundManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("fund/create")]
        public ResponseData<string> Create([FromBody] FundForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createFund(dto);
            return response;
        }

        [HttpDelete("fund/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteFund(id);
            return response;
        }

        [HttpPut("fund/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] FundForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateFund(id, dto);
            return response;
        }

        [HttpGet("fund/get-all")]
        public ResponseData<List<Fund>> GetAll([FromQuery] FundForSearchDTO dto)
        {
            ResponseData<List<Fund>> response = _repository.getAllFund(dto);
            return response;
        }

        [HttpGet("fund/getFundId/{id}")]
        public ResponseData<Fund> GetFundById(String id)
        {
            ResponseData<Fund> response = _repository.getFundById(id);
            return response;
        }

        [HttpGet("fund/export-data/{buildingId}")]
        public IActionResult ExportData(string buildingId)
        {
            try
            {
                byte[] fileContents = _repository.ExportData(buildingId);

                // Return the Excel file as a downloadable attachment
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "funds.xlsx");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Failed to export funds. Reason: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to export funds.");
            }
        }
    }
}
