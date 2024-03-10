using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class MemberManagerController : ControllerBase
    {
        private IMemberManagerRepository _repository;

        public MemberManagerController(IMemberManagerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("resident-room-member/get")]
        public ResponseData<List<Resident>> GetAllMember([FromQuery]MemberForSearchDTO dto)
        {
            ResponseData<List<Resident>> response = _repository.getAllMember(dto);
            return response;
        }

        [HttpGet("resident-room-member/get/{id}")]
        public ResponseData<Resident> GetMemberById(String id)
        {
            ResponseData<Resident> response = _repository.getMemberById(id);
            return response;
        }

        [HttpPost("resident-room-member/create")]
        public ResponseData<string> Create([FromBody] MemberForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createMember(dto);
            return response;
        }

        [HttpPut("resident-room-member/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] MemberForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateMember(id, dto);
            return response;
        }

        [HttpDelete("resident-room-member/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteMember(id);
            return response;
        }
    }
}
