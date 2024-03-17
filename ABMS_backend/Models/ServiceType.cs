using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Loại dịch vụ
    /// </summary>
    public partial class ServiceType
    {
        public ServiceType()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; } = null!;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string? ModifyUser { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
        public string? BuildingId { get; set; }

        public virtual Building? Building { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
