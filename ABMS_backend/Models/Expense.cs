using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Chi
    /// </summary>
    public partial class Expense
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tổng chi
        /// </summary>
        public float Expense1 { get; set; }
        /// <summary>
        /// Số tiền chi
        /// </summary>
        public float? Step { get; set; }
        /// <summary>
        /// Nguồn chi
        /// </summary>
        public string? ExpenseSource { get; set; }
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
