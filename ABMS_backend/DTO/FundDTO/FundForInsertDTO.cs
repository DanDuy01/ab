namespace ABMS_backend.DTO.FundDTO
{
    public class FundForInsertDTO
    {
        public string buildingId { get; set; }
        public float fund { get; set; }
        public string fundSource { get; set; }
        public string description { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }
            return null;
        }

            
    }
}
