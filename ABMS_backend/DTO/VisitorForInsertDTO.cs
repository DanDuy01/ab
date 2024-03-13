using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ABMS_backend.DTO
{
    public class VisitorForInsertDTO
    {
        public string roomId { get; set; }
        public string fullName { get; set; }
        public DateTime arrivalTime { get; set; }
        public DateTime departureTime { get; set; }
        public bool gender { get; set; }
        
        [Phone]
        public string phoneNumber { get; set; } 
        
        public string identityNumber { get; set; } 
        public string identityCardImgUrl { get; set; } 
        public string? description { get; set; }
        //public string? approveUser { get; set; }

        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);

            if (string.IsNullOrEmpty(fullName))
            {
                return "Name is required!";
            }
            if (!regexPhone.IsMatch(phoneNumber))
            {
                return "Wrong phone!";
            }          

           

            return null;
        }
    }
}
