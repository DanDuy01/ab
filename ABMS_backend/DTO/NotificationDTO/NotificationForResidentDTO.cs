namespace ABMS_backend.DTO.NotificationDTO
{
    public class NotificationForResidentDTO
    {


        public string title { get; set; }

        public string content { get; set; }

        public string buildingId { get; set; }

        public string roomId { get; set; }
        public string Validate()
        {

         
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
