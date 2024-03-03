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

        [HttpPost("create-account/create")]
        public ResponseData<string> Create([FromBody] dtotest dto)
        {
            ResponseData<string> response = _repository.Register(dto);
            return response;
        }

        [HttpPost("login-account/loginByPhone")]
        public ResponseData<string> LoginByPhone([FromBody] Login dto)
        {
            ResponseData<string> response = _repository.getAccount(dto);
            return response;
        }
        
        [HttpPost("login-account/loginByEmail")]
        public ResponseData<string> LoginByEmail([FromBody] LoginWithEmail dto)
        {
            ResponseData<string> response = _repository.getAccountByEmail(dto);
            return response;
        }
    }
}
