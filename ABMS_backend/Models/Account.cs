using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Tài khoản
    /// </summary>
    public partial class Account
    {
        public Account()
        {
            AccountPosts = new HashSet<AccountPost>();
            Rooms = new HashSet<Room>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Mã tòa nhà
        /// </summary>
        public string BuildingId { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mật khẩu salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
        /// <summary>
        /// Mật khẩu hash
        /// </summary>
        public byte[] PasswordHash { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Vai trò: 0 admin, 1 cmb, 2 lễ tân, 3 room
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
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
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }

        public virtual ICollection<AccountPost> AccountPosts { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
