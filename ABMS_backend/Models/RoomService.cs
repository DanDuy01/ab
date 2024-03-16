using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Lịch sử dùng dịch vụ
    /// </summary>
    public partial class RoomService
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
        /// Mã dịch vụ
        /// </summary>
        public string FeeId { get; set; } = null!;
        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateOnly Date { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; } = null!;
    }
}
