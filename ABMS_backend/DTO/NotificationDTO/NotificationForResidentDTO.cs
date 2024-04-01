namespace ABMS_backend.DTO.NotificationDTO
{
    public class NotificationForResidentDTO
    {
        public string account_id { get; set; }

        public string service_id { get; set; }

        public string service_name { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(account_id))
            {
                return "Account is required!";
            }

            if (string.IsNullOrEmpty(service_id))
            {
                return "Service id plate is required!";
            }

            if (string.IsNullOrEmpty(service_name))
            {
                return "Service name is required!";
            }

            if (string.IsNullOrEmpty(title))
            {
                return "Title is required!";
            }

            if (string.IsNullOrEmpty(content))
            {
                return "Content is required!";
            }
            return null;
        }
    }
}
