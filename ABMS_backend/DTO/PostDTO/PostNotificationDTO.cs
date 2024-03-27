using ABMS_backend.Models;

namespace ABMS_backend.DTO.PostDTO
{
    public class PostNotificationDTO
    {
        public Post Post { get; set; }
        public bool IsRead { get; set; }
    }
}
