using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Khách thăm
    /// </summary>
    public partial class Visitor
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
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Ngày, giờ đến
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// Ngày, giờ đi
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Số cccd
        /// </summary>
        public string IdentityNumber { get; set; } = null!;
        /// <summary>
        /// Ảnh cccd
        /// </summary>
        public string IdentityCardImgUrl { get; set; } = null!;
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
