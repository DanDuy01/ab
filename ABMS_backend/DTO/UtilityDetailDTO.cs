using ABMS_backend.Models;

namespace ABMS_backend.DTO
{
    public class UtilityDetailDTO
    {
        public string name { get; set; }

        public string utility_id { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(name))
            {
                return "Name is required!";
            }

            else if (String.IsNullOrEmpty(utility_id))
            {
                return "Utility is required!";
            }

            return null;
        }
    }
}
