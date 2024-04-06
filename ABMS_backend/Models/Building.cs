using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Tòa nhà
    /// </summary>
    public partial class Building
    {
        public Building()
        {
            Accounts = new HashSet<Account>();
            Expenses = new HashSet<Expense>();
            Fees = new HashSet<Fee>();
            Funds = new HashSet<Fund>();
            Hotlines = new HashSet<Hotline>();
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
            Rooms = new HashSet<Room>();
            ServiceTypes = new HashSet<ServiceType>();
            Utilities = new HashSet<Utility>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tên tòa nhà
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; } = null!;
        /// <summary>
        /// Số tầng
        /// </summary>
        public int NumberOfFloor { get; set; }
        /// <summary>
        /// Số căn mỗi tầng
        /// </summary>
        public int RoomEachFloor { get; set; }
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

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Fee> Fees { get; set; }
        public virtual ICollection<Fund> Funds { get; set; }
        public virtual ICollection<Hotline> Hotlines { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }
    }
}
