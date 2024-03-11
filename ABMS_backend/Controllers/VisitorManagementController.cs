﻿using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]

    public class VisitorManagementController : ControllerBase
    {
        private IVisitorManagementRepository _repository;

        public VisitorManagementController(IVisitorManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("visitor/create")]
        public ResponseData<string> Create([FromBody] VisitorForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createRequestVisitor(dto);
            return response;
        }

        [HttpPut("visitor/manage/{id}")]
        public ResponseData<string> Manage(String id, int status)
        {
            ResponseData<string> response = _repository.manageVisitor(id, status);
            return response;
        }

        [HttpPut("visitor/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] VisitorForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updateRequestVisitor(id, dto);
            return response;
        }

        [HttpDelete("visitor/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteRequestVisitor(id);
            return response;
        }

        [HttpGet("visitor/get-all")]
        public ResponseData<List<Visitor>> GetAll([FromQuery] VisitorForSearchDTO dto)
        {
            ResponseData<List<Visitor>> response = _repository.getAllRequestVisitor(dto);
            return response;
        }
        
        [HttpGet("visitor/getVisitorbyId/{id}")]
        public ResponseData<Visitor> GetVisitorById(String id)
        {
            ResponseData<Visitor> response = _repository.getRequestVisitorById(id);
            return response;
        }

    }
}
