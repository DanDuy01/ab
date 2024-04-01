using ABMS_backend.DTO.BuildingDTO;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.Models;

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
        public ResponseData<string> CreateForReceptionist([FromBody] NotificationForRecepionistDTO dto)
        {
            ResponseData<string> response = _repository.createNotificationForReceptionist(dto);
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
        public ResponseData<List<Notification>> Get(string account_id, bool? isRead)
        {
            ResponseData<List<Notification>> response = _repository.getNotification(account_id, isRead);
            return response;
        }
    }
}
