﻿using ABMS_backend.DTO;
using ABMS_backend.DTO.UtilityDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
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

        [HttpGet("{utilityId}/has-schedules")]
        public IActionResult CheckUtilityDetailsHaveSchedules(string utilityId)
        {
            try
            {
                var response = _repository.CheckUtilityDetailsHaveSchedules(utilityId);
                if (response.Data)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseData<bool>
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrMsg = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpGet("utility/get-all")]
        public ResponseData<List<Utility>> GetAllUtility([FromQuery] UtilityForSearch dto)
        {
            ResponseData<List<Utility>> response = _repository.getAllUtility(dto);
            return response;
        }

        [HttpGet("utility/get/{id}")]
        public ResponseData<Utility> GetUtilityById(String id)
        {
            ResponseData<Utility> response = _repository.getUtilityById(id);
            return response;
        }

        [HttpPut("utility/restore")]
        public ResponseData<string> Restore(List<String> idList)
        {
            ResponseData<string> response = _repository.restore(idList);
            return response;
        }

        [HttpDelete("utility/remove")]
        public ResponseData<string> Remove(List<String> idList)
        {
            ResponseData<string> response = _repository.remove(idList);
            return response;
        }

        [HttpPost("utility/create-utility-detail")]
        public ResponseData<string> CreateUtilityDetail([FromBody] UtilityDetailDTO dto)
        {
            ResponseData<string> response = _repository.createUtilityDetail(dto);
            return response;
        }

        [HttpPut("utility/update-utility-detail/{id}")]
        public ResponseData<string> UpdateDetail(String id, String name)
        {
            ResponseData<string> response = _repository.updateUtilityDetail(id, name);
            return response;
        }

        [HttpDelete("utility/delete-utility-detail/{id}")]
        public ResponseData<string> DeleteDetail(String id)
        {
            ResponseData<string> response = _repository.deleteUtilityDetail(id);
            return response;
        }

        [HttpGet("utility/get-utility-detail")]
        public ResponseData<List<UtiliityDetail>> GetUtilityDetail(String? utilityId)
        {
            ResponseData<List<UtiliityDetail>> response = _repository.getUtilityDetail(utilityId);
            return response;
        }

        [HttpGet("utility/get-utility-detail/{id}")]
        public ResponseData<UtiliityDetail> GetUtilityDetailById(String id)
        {
            ResponseData<UtiliityDetail> response = _repository.getUtilityDetailById(id);
            return response;
        }
    }
}
