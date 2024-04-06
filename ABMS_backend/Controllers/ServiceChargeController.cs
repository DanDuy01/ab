﻿using ABMS_backend.DTO.ParkingCardDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.ServiceChargeDTO;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ServiceChargeController : ControllerBase
    {
        private IServiceChargeRepository _repository;

        public ServiceChargeController(IServiceChargeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("service-charge/create")]
        public ResponseData<string> Create([FromBody] ServiceChargeForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createServiceCharge(dto);
            return response;
        }

        [HttpPut("service-charge/update/{id}")]
        public ResponseData<string> Update(String id, string? description, int? status)
        {
            ResponseData<string> response = _repository.updateServiceCharge(id, description, status);
            return response;
        }

        [HttpDelete("service-charge/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteServiceCharge(id);
            return response;
        }

        [HttpGet("service-charge/get")]
        public ResponseData<List<ServiceChargeListResponseDTO>> Get([FromQuery] ServiceChargeForSearchDTO dto)
        {
            ResponseData<List<ServiceChargeListResponseDTO>> response = _repository.getServiceCharge(dto);
            return response;
        }

        [HttpGet("service-charge/get/{id}")]
        public ResponseData<ServiceCharge> GetById(String id)
        {
            ResponseData<ServiceCharge> response = _repository.getServiceChargeById(id);
            return response;
        }

        [HttpGet("service-charge/get-total/{room_id}")]
        public ResponseData<List<ServiceChargeResponseDTO>> getToTal(String room_id, int? status)
        {
            ResponseData<List<ServiceChargeResponseDTO>> response = _repository.getTotal(room_id, status);
            return response;
        }

        [HttpGet("service-charge/export-data/{buildingId}")]
        public IActionResult ExportData(string buildingId)
        {
            try
            {
                byte[] fileContents = _repository.ExportData(buildingId);

                // Return the Excel file as a downloadable attachment
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "service-charges.xlsx");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Failed to export service-charge. Reason: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to export service-charges.");
            }
        }
    }
}
