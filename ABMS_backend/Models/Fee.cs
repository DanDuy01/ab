using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Bảng giá dịch vụ
    /// </summary>
    public partial class Fee
    {
        public Fee()
        {
            ServiceCharges = new HashSet<ServiceCharge>();
        }

        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Mã tòa nhà
        /// </summary>
        public string? BuildingId { get; set; }
        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        public string ServiceName { get; set; } = null!;
        /// <summary>
        /// Giá
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string Unit { get; set; } = null!;
        /// <summary>
        /// Ngày có hiệu lực
        /// </summary>
        public DateOnly EffectiveDate { get; set; }
        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateOnly? ExpireDate { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
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

        public virtual Building? Building { get; set; }
        public virtual ICollection<ServiceCharge> ServiceCharges { get; set; }
    }
}
