using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class ConstructionInsertDTO
    {
        public string roomId {  get; set; }
        public string name { get; set; }
        public string constructionOrganization { get; set; }
        [Phone]
        public string phone { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string? description { get; set; }

        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);

            if (!regexPhone.IsMatch(phone))
            {
                return "Invalid phone number!";
            }

            if(String.IsNullOrEmpty(roomId))
            {
                return "Room is required!";
            }

            if (endTime < startTime)
            {
                return "End time must after start time!";
            }

            return null;
        }
    }
}
