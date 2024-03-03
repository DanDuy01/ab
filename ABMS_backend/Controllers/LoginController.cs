using Microsoft.AspNetCore.Mvc;
using ABMS_backend.Utils.Validates;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Models;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginAccount _repository;

        public LoginController(ILoginAccount repository)
        {
            _repository = repository;
        }

        [HttpPost("account/register")]
        public ResponseData<string> Create([FromBody] RegisterDTO dto)
        {
            ResponseData<string> response = _repository.Register(dto);
            return response;
        }

        [HttpPost("account/loginByPhone")]
        public ResponseData<string> LoginByPhone([FromBody] Login dto) 
        {
            ResponseData<string> response = _repository.getAccount(dto);
            return response;
        }
        
        [HttpPost("account/loginByEmail")]
        public ResponseData<string> LoginByEmail([FromBody] LoginWithEmail dto)
        {
            ResponseData<string> response = _repository.getAccountByEmail(dto);
            return response;
        }
    }
}
