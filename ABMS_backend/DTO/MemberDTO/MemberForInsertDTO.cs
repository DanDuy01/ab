using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO.MemberDTO
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
            if (string.IsNullOrEmpty(roomId))
            {
                return "Room is required!";
            }
            if (!regexPhone.IsMatch(phone))
            {
                return "Wrong phone!";
            }
            if (string.IsNullOrEmpty(fullName))
            {
                return "Full name is required!";
            }

            return null;
        }
    }
}
