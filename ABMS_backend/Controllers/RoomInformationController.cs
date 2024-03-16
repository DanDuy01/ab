using ABMS_backend.DTO;
using ABMS_backend.DTO.RoomDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class RoomInformationController : ControllerBase
    {
        private IRoomInformationRepository _repository;

        public RoomInformationController(IRoomInformationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("resident-room/get")]
        public ResponseData<List<RoomBuildingResponseDTO>> Get([FromQuery]RoomForSearchDTO dto)
        {
            ResponseData<List<RoomBuildingResponseDTO>> response = _repository.getRoomInformation(dto);
            return response;
        }

        [HttpGet("resident-room/get/{id}")]
        public ResponseData<Room> GetRoomById(String id)
        {
            ResponseData<Room> response = _repository.getRoomInformationById(id);
            return response;
        }


        [HttpPost("resident-room/create")]
        public ResponseData<string> Create([FromBody] RoomForCreateDTO dto)
        {
            ResponseData<string> response = _repository.createRoomInformation(dto);
            return response;
        }

        [HttpPut("resident-room/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] RoomForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateRoomInformation(id, dto);
            return response;
        }

        [HttpDelete("resident-room/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteRoomInformation(id);
            return response;
        }
    }
}
