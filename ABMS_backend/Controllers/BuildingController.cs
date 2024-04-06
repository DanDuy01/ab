using ABMS_backend.DTO;
using ABMS_backend.DTO.BuildingDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private IBuildingRepository _repository;

        public BuildingController(IBuildingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("building/create")]
        public ResponseData<string> Create([FromBody] BuildingForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createBuilding(dto);
            return response;
        }

        [HttpPut("building/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] BuildingForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateBuilding(id, dto);
            return response;
        }

        [HttpDelete("building/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteBuilding(id);
            return response;
        }

        [HttpGet("building/get")]
        public ResponseData<List<Building>> Get([FromQuery] BuildingForSearchDTO dto)
        {
            ResponseData<List<Building>> response = _repository.getBuilding(dto);
            return response;
        }


        [HttpGet("building/get/{id}")]
        public ResponseData<Building> GetById(String id)
        {
            ResponseData<Building> response = _repository.getBuildingById(id);
            return response;
        }
    }
}
