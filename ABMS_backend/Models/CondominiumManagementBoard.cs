using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class CondominiumManagementBoard
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public string AccountId { get; set; } = null!;
        /// <summary>
        /// Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực
        /// </summary>
        public int Status { get; set; }
    }
}
