using ABMS_backend.DTO.ServiceChargeDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.RoomServiceDTO;
using ABMS_backend.Services;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class RoomServiceController : ControllerBase
    {
        private IRoomServiceRepository _repository;

        public RoomServiceController(IRoomServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("room-service/create")]
        public ResponseData<string> Create([FromBody] RoomServiceForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createRoomService(dto);
            return response;
        }

        [HttpDelete("room-service/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteRoomService(id);
            return response;
        }
        [HttpDelete("room-service/delete-by-roomfee")]
        public ResponseData<string> DeleteByFeeId(string feeId, string roomId)
        {
            ResponseData<string> response = _repository.deleteByFeeId(feeId, roomId);
            return response;
        }

        [HttpDelete("room-service/delete-draft")]
        public ResponseData<string> DeleteDraft(List<String> idList)
        {
            ResponseData<string> response = _repository.deleteDraftRoomService(idList);
            return response;
        }

        [HttpGet("room-service/get")]
        public ResponseData<List<RoomService>> Get([FromQuery] RoomServiceForSearchDTO dto)
        {
            ResponseData<List<RoomService>> response = _repository.getRoomService(dto);
            return response;
        }

        [HttpGet("room-service/get/{id}")]
        public ResponseData<RoomService> GetById(String id)
        {
            ResponseData<RoomService> response = _repository.getRoomServiceById(id);
            return response;
        }
        [HttpDelete("DeleteRoomServicesInBuilding/{buildingId}")]
        public ActionResult DeleteRoomServicesInBuilding(string buildingId)
        {
            try
            {
                var result =_repository.DeleteRoomServicesInBuilding(buildingId);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = $"An error occurred while deleting RoomServices: {ex.Message}"
                });
            }
        }
        [HttpGet("CheckUnassignedRoomServicesInBuilding/{buildingId}")]
        public ActionResult CheckUnassignedRoomServicesInBuilding(string buildingId)
        {
            try
            {
                var result = _repository.CheckUnassignedRoomServicesInBuilding(buildingId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseData<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = $"An error occurred while checking for unassigned rooms: {ex.Message}"
                });
            }
        }
    }
}
