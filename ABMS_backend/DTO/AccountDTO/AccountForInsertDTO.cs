using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO.AccountDTO
{
    public class AccountForInsertDTO
    {
        public string building_id { get; set; }

        [Phone]
        public string phone { get; set; }

        [EmailAddress]
        public string email { get; set; }


        public string user_name { get; set; }

        public int role { get; set; }


        public string full_name { get; set; }


        public string? avatar { get; set; }


        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";

            Regex regexPhone = new Regex(phoneRegexPattern);

            string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regexEmail = new Regex(emailRegexPattern);

            if (string.IsNullOrEmpty(building_id))
            {
                return "Building is required!";
            }

            else if (!regexPhone.IsMatch(phone))
            {
                return "Wrong phone!";
            }

            else if (!regexEmail.IsMatch(email))
            {
                return "Wrong email!";
            }

            else if (string.IsNullOrEmpty(full_name))
            {
                return "Full name is required!";
            }

            else if (string.IsNullOrEmpty(user_name))
            {
                return "Full name is required!";
            }

            return null;
        }
    }
}
