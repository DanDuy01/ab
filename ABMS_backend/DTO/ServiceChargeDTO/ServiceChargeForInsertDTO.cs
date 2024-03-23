using System.Xml.Linq;

namespace ABMS_backend.DTO.ServiceChargeDTO
{
    public class ServiceChargeForInsertDTO
    {
        public string room_id { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public string? description { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(room_id))
            {
                return "Room is required!";
            }

            else if (month < 1 || month > 12)
            {
                return "Wrong month!";
            }
            
            else if (year < DateTime.Now.Year)
            {
                return "Wrong year!";
            }

            return null;
        }
    }
}
