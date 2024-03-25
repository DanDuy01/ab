using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO.HotlineDTO
{
    public class HotlineForInsertDTO
    {
        [Phone]
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string buildingId { get; set; }
        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);
            if (!regexPhone.IsMatch(phoneNumber))
            {
                return "Wrong phone!";
            }
            if (string.IsNullOrEmpty(name))
            {
                return "Full name is required!";
            }
            if (string.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }
            return null;
        }
    }
}
