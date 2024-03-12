using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    /// <summary>
    /// Thi công
    /// </summary>
    public partial class Construction
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Mã căn hộ
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// Tên thi công
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Đơn vị thi công
        /// </summary>
        public string ConstructionOrganization { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string PhoneContact { get; set; }
        /// <summary>
        /// Giờ bắt đầu
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Giờ kết thúc
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Người phê duyệt
        /// </summary>
        public string ApproveUser { get; set; }
        /// <summary>
        /// Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối
        /// </summary>
        public int Status { get; set; }

        public virtual Room Room { get; set; }
    }
}
