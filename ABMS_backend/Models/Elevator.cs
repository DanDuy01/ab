using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thang máy
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
        /// Số giờ dự kiến
        /// </summary>
        public TimeOnly PlanTotalTime { get; set; }
        /// <summary>
        /// Số giờ thực tế
        /// </summary>
        public TimeOnly? ActualTotalTime { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string? ApproveUser { get; set; }
        /// <summary>
        /// Trạng thái: 0 đã gửi, 1 đã duyệt, 2 bị từ chối
        /// </summary>
        public int Status { get; set; }
    }
}
