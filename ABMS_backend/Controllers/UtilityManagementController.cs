using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class UtilityManagementController : ControllerBase
    {
        private IUtilityManagementRepository _repository;

        public UtilityManagementController(IUtilityManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("utility/create")]
        public ResponseData<string> Create([FromBody] UtilityForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createUtility(dto);
            return response;
        }

        [HttpPut("utility/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] UtilityForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateUtility(id, dto);
            return response;
        }

        [HttpDelete("utility/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteUtility(id);
            return response;
        }

        [HttpGet("utility/get-all")]
        [Authorize(Roles = "1")]
        public ResponseData<List<Utility>> GetAll(UtilityForSearch dto)
        {
            ResponseData<List<Utility>> response = _repository.getAllUtility(dto);
            return response;
        }

        [HttpGet("utility/get")]
        public ResponseData<List<UtilityForInsertDTO>> Get(UtilityForSearch dto)
        {
            ResponseData<List<UtilityForInsertDTO>> response = _repository.getUtility(dto);
            return response;
        }


        [HttpGet("utility/get/{id}")]
        public ResponseData<Utility> GetById(String id)
        {
            ResponseData<Utility> response = _repository.getUtilityById(id);
            return response;
        }
    }
}
