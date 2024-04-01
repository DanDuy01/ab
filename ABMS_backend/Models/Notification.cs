using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thông báo
    /// </summary>
    public partial class Notification
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public string AccountId { get; set; } = null!;
        /// <summary>
        /// Mã dịch vụ hoặc tiện ích
        /// </summary>
        public string ServiceId { get; set; } = null!;
        /// <summary>
        /// Tên dịch vụ hoặc tiện ích
        /// </summary>
        public string ServiceName { get; set; } = null!;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Đã đọc: true, false
        /// </summary>
        public bool IsRead { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
