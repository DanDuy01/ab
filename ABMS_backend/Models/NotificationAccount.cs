using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class NotificationAccount
    {
        public string Id { get; set; } = null!;
        public string? NotificationId { get; set; }
        public string? AccountId { get; set; }
        public sbyte? IsRead { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Notification? Notification { get; set; }
    }
}
