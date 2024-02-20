using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Căn hộ
    /// </summary>
    public partial class Room
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
        /// Số nhà
        /// </summary>
        public string RoomNumber { get; set; } = null!;
        /// <summary>
        /// Số thành viên
        /// </summary>
        public int? NumberOfResident { get; set; }
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
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
