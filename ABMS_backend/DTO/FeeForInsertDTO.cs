namespace ABMS_backend.DTO
{
    public class FeeForInsertDTO
    {
        public string feeName { get; set; }
        public int price { get; set; }
        public string unit { get; set; }
        public DateOnly effectiveDate { get; set; }
        public string description { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(feeName))
            {
                return "Fee name is required!";
            }
            if (price <= 0)
            {
                return "Price must be greater than zero!";
            }
            return null;
        }
    }
}
