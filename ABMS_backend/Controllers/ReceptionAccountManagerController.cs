using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ReceptionAccountManagerController : ControllerBase
    {
        private IReceptionistAccountRepository _repository;

        public ReceptionAccountManagerController(IReceptionistAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("reception-account/create")]
        public ResponseData<String> Create([FromBody] AccountForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createReceptionAccount(dto);
            return response;
        }

        [HttpPut("reception-account/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] AccountForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateReceptionAccount(id, dto);
            return response;
        }

        [HttpDelete("reception-account/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteReceptionAccount(id);
            return response;
        }

        [HttpGet("reception-account/get")]
        public ResponseData<List<Account>> Get(AccountForSearchDTO dto)
        {
            ResponseData<List<Account>> response = _repository.getReceptionAccount(dto);
            return response;
        }


        [HttpGet("reception-account/get/{id}")]
        public ResponseData<Account> GetById(String id)
        {
            ResponseData<Account> response = _repository.getReceptionAccountById(id);
            return response;
        }
    }
}
