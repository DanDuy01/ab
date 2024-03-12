using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class AccountPost
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
        /// Mã bài đăng
        /// </summary>
        public string PostId { get; set; } = null!;
        /// <summary>
        /// Đã đọc: true, false
        /// </summary>
        public sbyte IsRead { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
