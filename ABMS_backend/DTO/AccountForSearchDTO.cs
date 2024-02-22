using System.ComponentModel.DataAnnotations;

namespace ABMS_backend.DTO
{
    public class AccountForSearchDTO
    {
        public string? apartmentId { get; set; }

        public string? phone { get; set; }

        public string? email { get; set; }

        public string? full_name { get; set; }

        public int? status { get; set; }
    }
}
