using Microsoft.AspNetCore.Mvc;
using ABMS_backend.Utils.Validates;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Models;
using ABMS_backend.DTO.AccountDTO;
using ABMS_backend.Services;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private IAccountManagementRepository _repository;

        public AccountManagementController(IAccountManagementRepository repository)
        {
            _repository = repository;
        }
        [HttpPut("account/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] AccountForUpdateDTO dto)
        {
            ResponseData<string> response = _repository.updateCmbAccount(id, dto);
            return response;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(string id)
        {
            var result = _repository.DeleteAccountAndRelatedData(id);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new { message = "Account and related data deleted successfully." });
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.ErrMsg);
            }
        }

        [HttpPut("account/active/{id}")]
        public ResponseData<string> Active(String id, int status)
        {
            ResponseData<string> response = _repository.activeAccount(id, status);
            return response;
        }

        [HttpDelete("account/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteCmbAccount(id);
            return response;
        }

        [HttpGet("account/get")]
        public ResponseData<List<Account>> Get([FromQuery] AccountForSearchDTO dto)
        {
            ResponseData<List<Account>> response = _repository.getCmbAccount(dto);
            return response;
        }


        [HttpGet("account/get/{id}")]
        public ResponseData<Account> GetById(String id)
        {
            ResponseData<Account> response = _repository.getCmbAccountById(id);
            return response;
        }
    }
}
