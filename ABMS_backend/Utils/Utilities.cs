namespace ABMS_backend.Utils
{
    public static class Utilities
    {
        public static string FormatToE164(string localNumber, string countryCode = "84")
        {
            if (localNumber.StartsWith("0"))
            {
                localNumber = localNumber.Substring(1);
            }
            return $"+{countryCode}{localNumber}";
        }
        public static string GetRandomNumber(int length)
        {
            string randomNumber = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                randomNumber += random.Next(0, 10).ToString();
            }
            return randomNumber;
        }
    }
}
