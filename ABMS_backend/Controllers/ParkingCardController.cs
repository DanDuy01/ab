using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ParkingCardController : ControllerBase
    {
        private IParkingCardRepository _repository;

        public ParkingCardController(IParkingCardRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("parking-card/create")]
        public ResponseData<string> Create([FromBody] ParkingCardForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createParkingCard(dto);
            return response;
        }

        [HttpPut("parking-card/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ParkingCardForEditDTO dto)
        {
            ResponseData<string> response = _repository.updateParkingCard(id, dto);
            return response;
        }

        [HttpDelete("parking-card/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteParkingCard(id);
            return response;
        }

        [HttpGet("parking-card/get")]
        public ResponseData<List<ParkingCard>> Get([FromQuery] ParkingCardForSearchDTO dto)
        {
            ResponseData<List<ParkingCard>> response = _repository.getParkingCard(dto);
            return response;
        }


        [HttpGet("parking-card/get/{id}")]
        public ResponseData<ParkingCard> GetById(String id)
        {
            ResponseData<ParkingCard> response = _repository.getParkingCardById(id);
            return response;
        }
    }
}
