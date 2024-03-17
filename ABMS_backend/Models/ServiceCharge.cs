using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thanh toán dịch vụ
    /// </summary>
    public partial class ServiceCharge
    {
        /// <summary>
        /// Khóa chỉnh của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        public string RoomId { get; set; } = null!;
        /// <summary>
        /// Số tiền
        /// </summary>
        public float Fee { get; set; }
        /// <summary>
        /// Tháng
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Năm
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; } = null!;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string? ModifyUser { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Trạng thái: 5 đã thanh toán, 6 chưa thanh toán
        /// </summary>
        public int Status { get; set; }
        public string? FeeId { get; set; }

        public virtual Fee? FeeNavigation { get; set; }
        public virtual Room Room { get; set; } = null!;
    }
}
