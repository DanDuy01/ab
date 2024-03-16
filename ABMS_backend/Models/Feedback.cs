using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// phản hồi
    /// </summary>
    public partial class Feedback
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        public string RoomId { get; set; } = null!;
        /// <summary>
        /// Mã loại dịch vụ
        /// </summary>
        public string ServiceTypeId { get; set; } = null!;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual ServiceType ServiceType { get; set; } = null!;
    }
}
