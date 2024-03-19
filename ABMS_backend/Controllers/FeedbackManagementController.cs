using ABMS_backend.DTO;
using ABMS_backend.DTO.ElevatorDTO;
using ABMS_backend.DTO.FeedbackDTO;
using ABMS_backend.DTO.VisitorDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class FeedbackManagementController : ControllerBase
    {
        private IFeedbackManagementRepository _repository;

        public FeedbackManagementController(IFeedbackManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("feedback/create")]
        public ResponseData<string> Create([FromBody] FeedbackInsert dto)
        {
            ResponseData<string> response = _repository.createFeedback(dto);
            return response;
        }

        [HttpDelete("feedback/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deleteFeedback(id);
            return response;
        }

        [HttpPut("feedback/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] FeedbackInsert dto)
        {
            ResponseData<string> response = _repository.updateFeedback(id, dto);
            return response;
        }
        [HttpPut("feedback/manage/{id}")]
        public ResponseData<string> Manage(String id, [FromBody] FeedbackForManageDTO dto)
        {
            ResponseData<string> response = _repository.replyFeedback(id, dto);
            return response;
        }


        [HttpGet("feedback/get-all")]
        public ResponseData<List<Feedback>> GetAll([FromQuery] FeedbackForSearch dto)
        {
            ResponseData<List<Feedback>> response = _repository.getAllFeedback(dto);
            return response;
        }

        [HttpGet("feedback/getFeedbackById/{id}")]
        public ResponseData<Feedback> GetFeedbackById(String id)
        {
            ResponseData<Feedback> response = _repository.getFeedbackById(id);
            return response;
        }
    }
}
