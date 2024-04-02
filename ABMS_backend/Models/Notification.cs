using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thông báo
    /// </summary>
    public partial class Notification
    {
        public Notification()
        {
            NotificationAccounts = new HashSet<NotificationAccount>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; } = null!;
        public string? BuildingId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Type { get; set; }

        public virtual Building? Building { get; set; }
        public virtual ICollection<NotificationAccount> NotificationAccounts { get; set; }
    }
}
