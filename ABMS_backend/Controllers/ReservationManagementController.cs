using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ReservationManagementController : ControllerBase
    {
        private IReservationManagementRepository _repository;

        public ReservationManagementController(IReservationManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("reservation/create")]
        public ResponseData<string> Create([FromBody] ReservationForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createReservation(dto);
            return response;
        }

        [HttpPut("reservation/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ReservationForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateReservation(id, dto);
            return response;
        }

        [HttpDelete("reservation/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteReservation(id);
            return response;
        }

        [HttpGet("reservation/get")]
        public ResponseData<List<ReservationResponseDTO>> GetAll([FromQuery] ResevationForResidentSearchDTO dto)
        {
            ResponseData<List<ReservationResponseDTO>> response = _repository.getReservation(dto);
            return response;
        }

        [HttpPut("reservation/manage/{id}")]
        public ResponseData<string> Manage(String id, int status)
        {
            ResponseData<string> response = _repository.manageReservation(id, status);
            return response;
        }


        [HttpGet("reservation/get/{id}")]
        public ResponseData<ReservationResponseDTO> GetById(String id)
        {
            ResponseData<ReservationResponseDTO> response = _repository.getReservationById(id);
            return response;
        }
    }
}
