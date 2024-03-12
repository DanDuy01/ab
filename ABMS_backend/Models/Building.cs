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
            Rooms = new HashSet<Room>();
            Utilities = new HashSet<Utility>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tên tòa nhà
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
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

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }
    }
}
