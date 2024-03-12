using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Bài viết
    /// </summary>
    public partial class Post
    {
        public Post()
        {
            AccountPosts = new HashSet<AccountPost>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Ảnh đính kèm
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string ModifyUser { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực, 2 đã gửi, 4 bị từ chối
        /// </summary>
        public int Status { get; set; }

        public virtual ICollection<AccountPost> AccountPosts { get; set; }
    }
}
