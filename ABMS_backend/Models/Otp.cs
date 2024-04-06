using System;
using System.Collections.Generic;

namespace ABMS_backend.Models
{
    public partial class Otp
    {
        public string Id { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string Otp1 { get; set; } = null!;
        public DateTime? Expire { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
