namespace ABMS_backend.DTO
{
    public class AccountForInsertDTO
    {
        public string id { get; set; }

        public string apartmentId { get; set; }

        public string phone { get; set; }

        public string pwd_salt { get; set; }

        public string pwd_hash { get; set; }

        public string email { get; set; }

        public string full_name { get; set; }

        public string avatar { get; set; }
    }
}
