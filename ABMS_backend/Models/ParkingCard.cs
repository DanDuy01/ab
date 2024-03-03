using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thẻ gửi xe
    /// </summary>
    public partial class ParkingCard
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã cư dân
        /// </summary>
        public string ResidentId { get; set; } = null!;
        /// <summary>
        /// Biển số xe
        /// </summary>
        public string LicensePlate { get; set; } = null!;
        /// <summary>
        /// Nhãn hiệu
        /// </summary>
        public string Brand { get; set; } = null!;
        /// <summary>
        /// Màu
        /// </summary>
        public string Color { get; set; } = null!;
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateOnly? ExpireDate { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; } = null!;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string? ModifyUser { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực, 2 đã gửi, 5 chưa thanh toán
        /// </summary>
        public int Status { get; set; }

        public virtual Resident Resident { get; set; } = null!;
    }
}
