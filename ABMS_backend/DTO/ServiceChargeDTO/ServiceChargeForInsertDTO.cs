using System.Xml.Linq;

namespace ABMS_backend.DTO.ServiceChargeDTO
{
    public class ServiceChargeForInsertDTO
    {
        public string building_id { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(building_id))
            {
                return "Building is required!";
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
