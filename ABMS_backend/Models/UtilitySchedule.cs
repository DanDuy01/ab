using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Lịch tiện ích
    /// </summary>
    public partial class UtilitySchedule
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
        /// Mã tiện ích
        /// </summary>
        public string UtilityId { get; set; } = null!;
        /// <summary>
        /// Slot
        /// </summary>
        public int Slot { get; set; }
        /// <summary>
        /// Đặt ngày
        /// </summary>
        public DateOnly BookingDate { get; set; }
        /// <summary>
        /// Số người tham gia
        /// </summary>
        public int? NumberOfPerson { get; set; }
        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string? ApproveUser { get; set; }
        /// <summary>
        /// Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối, 5 đã thanh toán
        /// </summary>
        public int Status { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual Utility Utility { get; set; } = null!;
    }
}
