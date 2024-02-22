namespace ABMS_backend.DTO
{
    public class UtilityForInsertDTO
    {
        public string name { get; set; }

        public TimeOnly openTime { get; set; }

        public TimeOnly closeTime { get; set; }

        public int numberOfSlot { get; set; }

        public float pricePerSlot { get; set; }

        public string? description { get; set; }
    }
}
