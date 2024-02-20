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
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tổng quỹ
        /// </summary>
        public float Fund1 { get; set; }
        /// <summary>
        /// Số tiền cộng thêm
        /// </summary>
        public float? Step { get; set; }
        /// <summary>
        /// Nguồn quỹ
        /// </summary>
        public string? FundSource { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
