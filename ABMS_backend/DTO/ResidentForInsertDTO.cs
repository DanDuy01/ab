using ABMS_backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class ResidentForInsertDTO
    {
        public string roomId {  get; set; }
        public string fullName {  get; set; }
        public DateOnly dob {  get; set; }
        public bool gender {  get; set; }
        [Phone]
        public string phone {  get; set; }

        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);

            if (String.IsNullOrEmpty(roomId))
            {
                return "Room is required!";
            }
            else if (!regexPhone.IsMatch(phone))
            {
                return "Wrong phone!";
            }
            else if (String.IsNullOrEmpty(fullName))
            {
                return "Full name is required!";
            }

            return null;
        }
    }
}
