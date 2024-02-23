using Microsoft.AspNetCore.Mvc;
using ABMS_backend.Utils.Validates;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Models;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ResidentAccountManagementController : ControllerBase
    {
        private IResidentAccountManagementRepository _repository;
        public ResidentAccountManagementController(IResidentAccountManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("resident-account/create")]
        public ResponseData<string> Create([FromBody] ResidentForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createResidentAccount(dto);
            return response;
        }

        [HttpPut("resident-account/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ResidentForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateResidentAccount(id, dto);
            return response;
        }

        [HttpDelete("resident-account/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteResidentAccount(id);
            return response;
        }

        [HttpGet("resident-account/get")]
        public ResponseData<List<Resident>> Get(ResidentForSearchDTO dto)
        {
            ResponseData<List<Resident>> response = _repository.getResidentAccount(dto);
            return response;
        }

        [HttpGet("resident-account/get/{id}")]
        public ResponseData<Resident> GetById(String id)
        {
            ResponseData<Resident> response = _repository.getResidentById(id);
            return response;
        }
    }
}
