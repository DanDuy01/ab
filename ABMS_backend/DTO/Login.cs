using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class Login
    {
        public string phoneNumber {  get; set; }
        public string password { get; set; }
        public string Validate()
        {
            string phoneRegexPattern = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            Regex regexPhone = new Regex(phoneRegexPattern);

            if (!regexPhone.IsMatch(phoneNumber))
            {
                return "Wrong phone number!";
            }

            return null;
        }
    }
}
