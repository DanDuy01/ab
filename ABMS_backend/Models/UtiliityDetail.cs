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

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string UtilityId { get; set; } = null!;

        public virtual ICollection<UtilitySchedule> UtilitySchedules { get; set; }
    }
}
