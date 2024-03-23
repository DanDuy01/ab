using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Căn hộ
    /// </summary>
    public partial class Room
    {
        public Room()
        {
            Constructions = new HashSet<Construction>();
            Elevators = new HashSet<Elevator>();
            Feedbacks = new HashSet<Feedback>();
            Residents = new HashSet<Resident>();
            RoomServices = new HashSet<RoomService>();
            ServiceCharges = new HashSet<ServiceCharge>();
            UtilitySchedules = new HashSet<UtilitySchedule>();
            Visitors = new HashSet<Visitor>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        /// <summary>
        /// Mã tòa nhà
        /// </summary>
        public string BuildingId { get; set; } = null!;
        /// <summary>
        /// Số nhà
        /// </summary>
        public string RoomNumber { get; set; } = null!;
        /// <summary>
        /// Diện tích căn hộ
        /// </summary>
        public float RoomArea { get; set; }
        /// <summary>
        /// Số thành viên
        /// </summary>
        public int? NumberOfResident { get; set; }
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

        public virtual Account Account { get; set; } = null!;
        public virtual Building Building { get; set; } = null!;
        public virtual ICollection<Construction> Constructions { get; set; }
        public virtual ICollection<Elevator> Elevators { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
        public virtual ICollection<RoomService> RoomServices { get; set; }
        public virtual ICollection<ServiceCharge> ServiceCharges { get; set; }
        public virtual ICollection<UtilitySchedule> UtilitySchedules { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}
