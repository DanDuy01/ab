using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class AccountPost
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// Mã bài đăng
        /// </summary>
        public string PostId { get; set; }
        /// <summary>
        /// Đã đọc: true, false
        /// </summary>
        public sbyte IsRead { get; set; }

        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
    }
}
