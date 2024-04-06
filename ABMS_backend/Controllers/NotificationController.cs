using ABMS_backend.DTO.BuildingDTO;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.Models;
using ABMS_backend.DTO.PostDTO;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationRepository _repository;

        public NotificationController(INotificationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("notification/create-for-receptionist")]
        public async Task<ResponseData<string>> CreateForReceptionist([FromBody] NotificationForRecepionistDTO dto)
        {
            // Call the async method and await its result directly without wrapping it in a Task variable
            ResponseData<string> response = await _repository.createNotificationForReceptionistAsync(dto);
            return response;
        }

        [HttpPost("notification/create-for-resident")]
        public ResponseData<string> CreateForResident([FromBody] NotificationForResidentDTO dto)
        {
            ResponseData<string> response = _repository.createNotificationForResident(dto);
            return response;
        }

        [HttpPut("notification/update/{id}")]
        public ResponseData<string> Update(String id)
        {
            ResponseData<string> response = _repository.updateNotification(id);
            return response;
        }

        [HttpGet("notification/get")]
        public ResponseData<List<Notification>> Get(string building_id)
        {
            ResponseData<List<Notification>> response = _repository.getNotification(building_id);
            return response;
        }

        [HttpPost("notification/markAsRead")]
        public IActionResult MarkAsRead(string accountId)
        {
            _repository.MarkNotificationsAsRead(accountId);
            return Ok();
        }

        [HttpGet("notification/get-notification")]
        public IEnumerable<NotificationAccountDTO> GetNotifcation([FromQuery] string accountId,
            [FromQuery] int skip, [FromQuery] int take)
        {
            IEnumerable<NotificationAccountDTO> response = _repository.GetNotifications(accountId, skip, take);
            return response;
        }
    }
}
