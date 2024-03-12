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
        public string Id { get; set; }
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// Mã loại dịch vụ
        /// </summary>
        public string ServiceTypeId { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public float Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public int Content { get; set; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }

        public virtual Room Room { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
