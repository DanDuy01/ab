using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Bài viết
    /// </summary>
    public partial class Post
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã lễ tân
        /// </summary>
        public string ReceptionistId { get; set; } = null!;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Ảnh đính kèm
        /// </summary>
        public string? Image { get; set; }
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
