using System.ComponentModel.DataAnnotations;

namespace ABMS_backend.DTO
{
    public class LoginWithEmail
    {
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }
    }
}
