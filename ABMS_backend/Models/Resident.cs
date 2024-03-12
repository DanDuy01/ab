using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Cư dân
    /// </summary>
    public partial class Resident
    {
        public Resident()
        {
            ParkingCards = new HashSet<ParkingCard>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateOnly DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính: true nam, false nữ
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Chủ căn hộ: true, false
        /// </summary>
        public bool IsHouseholder { get; set; }
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

        public virtual Room Room { get; set; }
        public virtual ICollection<ParkingCard> ParkingCards { get; set; }
    }
}
