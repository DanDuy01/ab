using System.Drawing.Drawing2D;
using System.Drawing;

namespace ABMS_backend.DTO.NotificationDTO
{
    public class NotificationForRecepionistDTO
    {
        public string building_id { get; set; }

        public string service_id { get; set; } 
        
        public string service_name { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(building_id))
            {
                return "Building is required!";
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
