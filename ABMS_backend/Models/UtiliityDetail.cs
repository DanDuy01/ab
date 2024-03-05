using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class UtiliityDetail
    {
        public UtiliityDetail()
        {
            UtilitySchedules = new HashSet<UtilitySchedule>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tên detail
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mã tiện ích
        /// </summary>
        public string UtilityId { get; set; } = null!;
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
        /// Trạng thái: 0 hết hạn sử dụng, 1 còn hạn sử dụng
        /// </summary>
        public int Status { get; set; }

        public virtual Utility Utility { get; set; } = null!;
        public virtual ICollection<UtilitySchedule> UtilitySchedules { get; set; }
    }
}
