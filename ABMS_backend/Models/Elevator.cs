using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thang chuyển đồ
    /// </summary>
    public partial class Elevator
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
        /// Ngày, giờ bắt đầu
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Ngày, giờ kết thúc
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string? ApproveUser { get; set; }
        /// <summary>
        /// Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối
        /// </summary>
        public int Status { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
