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
        public string Id { get; set; }
        /// <summary>
        /// Tên detail
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mã tiện ích
        /// </summary>
        public string UtilityId { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string ModifyUser { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hạn sử dụng, 1 còn hạn sử dụng
        /// </summary>
        public int Status { get; set; }

        public virtual Utility Utility { get; set; }
        public virtual ICollection<UtilitySchedule> UtilitySchedules { get; set; }
    }
}
