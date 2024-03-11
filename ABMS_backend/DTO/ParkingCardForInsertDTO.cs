namespace ABMS_backend.DTO
{
    public class ParkingCardForInsertDTO
    {
        public string resident_id { get; set; }

        public string license_plate { get; set; }
       
        public string brand { get; set; }
        
        public string color { get; set; }

        public string? image { get; set; }

        public DateOnly? expire_date { get; set; }

        public string? note { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(resident_id))
            {
                return "Resident is required!";
            }

            if (String.IsNullOrEmpty(license_plate))
            {
                return "License plate is required!";
            }
            
            if (String.IsNullOrEmpty(brand))
            {
                return "Brand plate is required!";
            }

            if (String.IsNullOrEmpty(color))
            {
                return "Color is required!";
            }

            if(expire_date < DateOnly.FromDateTime(DateTime.Now))
            {
                return "Invalid expire date!";
            }
            return null;
        }
    }
}
