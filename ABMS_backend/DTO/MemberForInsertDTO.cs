using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class MemberForInsertDTO
    {
        public string roomId { get; set; }
        public string fullName { get; set; }
        public DateOnly dob { get; set; }
        public bool gender { get; set; }
        [Phone]
        public string phone { get; set; }

        public bool isHouseHolder { get; set; }

        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);
            var regexFomatDate = new Regex(@"^\d{1,2}/\d{1,2}/\d{4} \d{1,2}:\d{1,2}$");

            if (String.IsNullOrEmpty(roomId))
            {
                return "Room is required!";
            }
            if (!regexPhone.IsMatch(phone))
            {
                return "Wrong phone!";
            }
            if (String.IsNullOrEmpty(fullName))
            {
                return "Full name is required!";
            }
            if (!regexFomatDate.IsMatch(dob.ToString()))
            {
                return "Wrong fomat date! ";
            }

            return null;
        }
    }
}
