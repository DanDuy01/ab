using ABMS_backend.DTO;
using ABMS_backend.DTO.NotificationDTO;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface INotificationRepository
    {
        Task<ResponseData<string>> createNotificationForReceptionistAsync(NotificationForResidentDTO dto);

        ResponseData<string> createNotificationForResident(NotificationForResidentDTO dto);

        ResponseData<List<Notification>> getNotification(string? buildingId);

        ResponseData<string> updateNotification(string id);

        void MarkNotificationsAsRead(string accountId);

        IEnumerable<NotificationAccountDTO> GetNotifications(string accountId, int skip, int take);
    }
}
