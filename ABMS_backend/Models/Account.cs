using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Tài khoản
    /// </summary>
    public partial class Account
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã tòa nhà
        /// </summary>
        public string ApartmentId { get; set; } = null!;
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Mật khẩu salt
        /// </summary>
        public string PasswordSalt { get; set; } = null!;
        /// <summary>
        /// Mật khẩu hash
        /// </summary>
        public string PasswordHash { get; set; } = null!;
        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Avatar
        /// </summary>
        public string? Avatar { get; set; }
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
