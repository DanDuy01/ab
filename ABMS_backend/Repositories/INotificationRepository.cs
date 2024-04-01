using ABMS_backend.DTO;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface INotificationRepository
    {
        ResponseData<string> createNotificationForReceptionist(NotificationForRecepionistDTO dto);
        
        ResponseData<string> createNotificationForResident(NotificationForResidentDTO dto);

        ResponseData<List<Notification>> getNotification(string account_id, bool? isRead);

        ResponseData<string> updateNotification(string id);
    }
}
