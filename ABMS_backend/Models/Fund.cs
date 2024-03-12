using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Quỹ
    /// </summary>
    public partial class Fund
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Số quỹ
        /// </summary>
        public float Fund1 { get; set; }
        /// <summary>
        /// Nguồn quỹ
        /// </summary>
        public string FundSource { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
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
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
