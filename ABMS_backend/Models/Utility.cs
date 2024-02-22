using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Tiện ích
    /// </summary>
    public partial class Utility
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tên tiện ích
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Giờ mở cửa
        /// </summary>
        public TimeOnly OpenTime { get; set; }
        /// <summary>
        /// Giờ đóng cửa
        /// </summary>
        public TimeOnly CloseTime { get; set; }
        /// <summary>
        /// Số slot trong 1 ngày
        /// </summary>
        public int NumberOfSlot { get; set; }
        /// <summary>
        /// Giá tiện ích
        /// </summary>
        public float PricePerSlot { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Người đưa vào hệ thống
        /// </summary>
        public string CreateUser { get; set; } = null!;
        /// <summary>
        /// Ngày đưa vào hệ thống
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
        /// Trạng thái sử dụng: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
