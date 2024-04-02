using ABMS_backend.DTO;
using ABMS_backend.DTO.ExpenseDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private IExpenseRepository _repository;
        public ExpenseController(IExpenseRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("expense/create")]
        public ResponseData<Expense> Create([FromBody] ExpenseInsert dto)
        {
            ResponseData<Expense> response = _repository.createExpense(dto);
            return response;
        }

        [HttpPut("expense/update/{id}")]
        public ResponseData<string> Update(string id, [FromBody] ExpenseInsert dto)
        {
            ResponseData<string> response = _repository.updateExpense(id, dto);
            return response;
        }

        [HttpDelete("expense/delete/{id}")]
        public ResponseData<string> Delete(string id)
        {
            ResponseData<string> response = _repository.deleteExpense(id);
            return response;
        }

        [HttpGet("expense/get/{id}")]
        public ResponseData<Expense> GetById(string id)
        {
            ResponseData<Expense> response = _repository.getExpenseById(id);
            return response;
        }

        [HttpGet("expense/get-all")]
        public ResponseData<List<Expense>> GetAll([FromQuery] ExpenseSearch dto)
        {
            ResponseData<List<Expense>> response = _repository.getAllExpense(dto);
            return response;
        }

        [HttpGet("expense/export-data/{buildingId}")]
        public IActionResult ExportData(string buildingId)
        {
            try
            {
                byte[] fileContents = _repository.ExportData(buildingId);

                // Return the Excel file as a downloadable attachment
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expenses.xlsx");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Failed to export expenses. Reason: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to export expenses.");
            }
        }
    }
}
