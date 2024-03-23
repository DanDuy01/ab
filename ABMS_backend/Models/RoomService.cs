using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
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
        /// Mã giá dịch vụ
        /// </summary>
        public string FeeId { get; set; } = null!;
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Amount { get; set; }
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
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }

        public virtual Fee Fee { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
