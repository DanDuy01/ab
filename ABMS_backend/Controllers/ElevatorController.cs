using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        private IElevatorRepository _repository;

        public ElevatorController(IElevatorRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("elevator/create")]
        public ResponseData<string> Create([FromBody] ElevatorForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createElevator(dto);
            return response;
        }

        [HttpPut("elevator/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ElevatorForEditDTO dto)
        {
            ResponseData<string> response = _repository.updateElevator(id, dto);
            return response;
        }

        [HttpPut("elevator/manage/{id}")]
        public ResponseData<string> Manage(String id, int status)
        {
            ResponseData<string> response = _repository.manageElevator(id, status);
            return response;
        }

        [HttpDelete("elevator/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteElevator(id);
            return response;
        }

        [HttpGet("elevator/get")]
        public ResponseData<List<Elevator>> Get([FromQuery] ElevatorForSearchDTO dto)
        {
            ResponseData<List<Elevator>> response = _repository.getElevator(dto);
            return response;
        }


        [HttpGet("elevator/get/{id}")]
        public ResponseData<Elevator> GetById(String id)
        {
            ResponseData<Elevator> response = _repository.getElevatorById(id);
            return response;
        }
    }
}
