using ABMS_backend.DTO;
using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class FeeManagementController : ControllerBase
    {
        private IFeeManagementRepository _repository;

        public FeeManagementController(IFeeManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("fee/create")]
        public ResponseData<string> Create([FromBody] FeeForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createFee(dto);
            return response;
        }

        [HttpDelete("fee/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteFee(id);
            return response;
        }

        [HttpPut("fee/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] FeeForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateFee(id, dto);
            return response;
        }

        [HttpGet("fee/get-all")]
        public ResponseData<List<Fee>> GetAll([FromQuery] FeeForSearchDTO dto)
        {
            ResponseData<List<Fee>> response = _repository.getAllFee(dto);
            return response;
        }

        [HttpGet("fee/getFeeId/{id}")]
        public ResponseData<Fee> GetVisitorById(String id)
        {
            ResponseData<Fee> response = _repository.getFeeById(id);
            return response;
        }

        [HttpGet("CheckRoomsMissingFees/{buildingId}")]
        public ActionResult CheckRoomsMissingFees(string buildingId)
        {
            try
            {
                var result = _repository.CheckRoomsMissingFees(buildingId);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseData<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = $"An error occurred while checking for rooms missing fees: {ex.Message}"
                });
            }
        }

        [HttpPost("AssignFeesToBuildingRooms")]
        public ActionResult AssignFeesToBuildingRooms(string buildingId)
        {
            try
            {
                var result = _repository.AssignFeesToAllRoomsInBuilding(buildingId);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseData<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = $"An error occurred while assigning fees: {ex.Message}"
                });
            }
        }
    }
}
