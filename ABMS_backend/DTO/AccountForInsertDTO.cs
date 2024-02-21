namespace ABMS_backend.DTO
{
<<<<<<<< HEAD:ABMS_backend/DTO/AccountDTO.cs
    public class AccountDTO
========
    public class AccountForInsertDTO
>>>>>>>> ae8801e6f333eda5eeb0e6347c14a65027ee5e0b:ABMS_backend/DTO/AccountForInsertDTO.cs
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
