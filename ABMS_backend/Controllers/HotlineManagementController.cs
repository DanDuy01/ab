using ABMS_backend.DTO.PostDTO;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.HotlineDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class HotlineManagementController : ControllerBase
    {
        private IHotlineManagementRepository _repository;

        public HotlineManagementController(IHotlineManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("hotline/create")]
        public ResponseData<string> Create([FromBody] HotlineForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createHotline(dto);
            return response;
        }

        [HttpDelete("hotline/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteHotline(id);
            return response;
        }
        
        [HttpPut("hotline/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] HotlineForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateHotline(id, dto);
            return response;
        }

        [HttpGet("hotline/get-all")]
        public ResponseData<List<Hotline>> GetAll([FromQuery] HotlineForSearchDTO dto)
        {
            ResponseData<List<Hotline>> response = _repository.getAllHotline(dto);
            return response;
        }

        [HttpGet("post/gethotlineId/{id}")]
        public ResponseData<Hotline> GetHotlineById(String id)
        {
            ResponseData<Hotline> response = _repository.getHotlineById(id);
            return response;
        }

    }
}
