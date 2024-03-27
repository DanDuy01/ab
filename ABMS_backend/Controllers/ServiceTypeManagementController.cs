using ABMS_backend.DTO;
using ABMS_backend.DTO.FeedbackDTO;
using ABMS_backend.DTO.ServiceTypeDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ServiceTypeManagementController : ControllerBase
    {
        private IServiceTypeRepository _repository;
        public ServiceTypeManagementController(IServiceTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("service-type/create")]
        public ResponseData<ServiceType> Create([FromBody] ServiceTypeInsert dto)
        {
            ResponseData<ServiceType> response = _repository.createServiceType(dto);
            return response;
        }

        [HttpPut("service-type/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] ServiceTypeInsert dto)
        {
            ResponseData<string> response = _repository.updateServiceType(id, dto);
            return response;
        }

        [HttpDelete("service-type/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteServiceType(id);
            return response;
        }

        [HttpGet("service-type/get/{id}")]
        public ResponseData<ServiceType> GetById(string id)
        {
            ResponseData<ServiceType> response = _repository.getServiceTypeById(id);
            return response;
        }

        [HttpGet("service-type/get-all")]
        public ResponseData<List<ServiceType>> GetAll([FromQuery] ServiceTypeSearch dto)
        {
            ResponseData<List<ServiceType>> response = _repository.getAllServiceType(dto);
            return response;
        }
    }
}
