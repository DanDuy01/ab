namespace ABMS_backend.DTO.ParkingCardDTO
{
    public class ParkingCardForEditDTO
    {
        public string resident_id { get; set; }

        public string license_plate { get; set; }

        public string brand { get; set; }

        public string color { get; set; }

        public int type { get; set; }

        public string? image { get; set; }

        public DateOnly? expire_date { get; set; }

        public int status { get; set; }

        public string? note { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(resident_id))
            {
                return "Resident is required!";
            }

            if (string.IsNullOrEmpty(license_plate))
            {
                return "License plate is required!";
            }

            if (string.IsNullOrEmpty(brand))
            {
                return "Brand plate is required!";
            }

            if (string.IsNullOrEmpty(color))
            {
                return "Color is required!";
            }

            if (expire_date < DateOnly.FromDateTime(DateTime.Now))
            {
                return "Invalid expire date!";
            }
            return null;
        }
    }
}
