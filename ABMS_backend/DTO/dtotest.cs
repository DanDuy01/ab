using System.ComponentModel.DataAnnotations;

namespace ABMS_backend.DTO
{
    public class dtotest
    {
        public string building_id { get; set; }

        [Phone]
        public string phone { get; set; }

        //public string pwd_salt { get; set; }

        public string password { get; set; }

        [EmailAddress]
        public string email { get; set; }

        public string full_name { get; set; }

        public string? avatar { get; set; }
    }
}
