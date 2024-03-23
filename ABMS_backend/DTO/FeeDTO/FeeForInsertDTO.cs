namespace ABMS_backend.DTO.FeeDTO
{
    public class FeeForInsertDTO
    {
        public string feeName { get; set; }
        public string buildingId { get; set; }
        public int price { get; set; }
        public string unit { get; set; }
        public DateOnly effectiveDate { get; set; }
        public string description { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(feeName))
            {
                return "Fee name is required!";
            }
            if (string.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }
            if (price <= 0)
            {
                return "Price must be greater than zero!";
            }
            return null;
        }
    }
}
