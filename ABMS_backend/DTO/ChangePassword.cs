namespace ABMS_backend.DTO
{
    public class ChangePassword
    {
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string confirm_password { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(old_password))
            {
                return "Old password is required!";
            }
            else if (string.IsNullOrEmpty(new_password))
            {
                return "New password is required!";
            }
            else if (new_password != confirm_password)
            {
                return "Confirm password must be same the new password!";
            }

            return null;
        }
    }
}
