using ABMS_backend.DTO;
using ABMS_backend.DTO.ConstructionDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ConstructionManagementController : ControllerBase
    {
        private IConstructionManagementRepository _repository;
        public ConstructionManagementController(IConstructionManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("construction/create")]
        public ResponseData<string> Create([FromBody] ConstructionInsertDTO dto)
        {
            ResponseData<string> response = _repository.createConstruction(dto);
            return response;
        }

        [HttpDelete("construction/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteConstruction(id);
            return response;
        }

        [HttpPut("construction/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ConstructionInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateConstruction(id, dto);
            return response;
        }


        [HttpGet("construction/get-all")]
        public ResponseData<List<Construction>> Get([FromQuery] ConstructionForSearchDTO dto)
        {
            ResponseData<List<Construction>> response = _repository.getAllConstruction(dto);
            return response;
        }

        [HttpPut("construction/manage/{id}")]
        public ResponseData<string> Manage(String id, ConstructionForManageDTO dto)
        {
            ResponseData<string> response = _repository.manageConstruction(id, dto);

            return response;
        }

        [HttpGet("construction/get/{id}")]
        public ResponseData<Construction> getContructionById(String id)
        {
            ResponseData<Construction> response = _repository.getContructionById(id);
            return response;
        }

       
    }
}
