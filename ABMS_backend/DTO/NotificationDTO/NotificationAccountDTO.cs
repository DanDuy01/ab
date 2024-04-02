using ABMS_backend.Models;

namespace ABMS_backend.DTO.NotificationDTO
{
    public class NotificationAccountDTO
    {
        public Notification Notification { get; set; }
        public bool IsRead { get; set; }
    }
}
