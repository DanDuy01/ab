using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
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


    }
}
