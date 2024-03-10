using ABMS_backend.Models;

namespace ABMS_backend.DTO
{
    public class UtilityForInsertDTO
    {
        public string name { get; set; }

        public string buildingId { get; set; }

        public TimeOnly openTime { get; set; }

        public TimeOnly closeTime { get; set; }

        public int numberOfSlot { get; set; }

        public float pricePerSlot { get; set; }

        public string? description { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(name))
            {
                return "Name is required!";
            }

            else if (String.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }

            else if (openTime == null)
            {
                return "Open time is required!";
            }

            else if (closeTime == null)
            {
                return "Close time is required!";
            }

            else if (numberOfSlot == null)
            {
                return "Number of slot is required!";
            }

            else if (numberOfSlot <= 0)
            {
                return "Number of slot must greater than 0!";
            }

            else if (pricePerSlot == null)
            {
                return "Price per slot is required!";
            }

            else if(pricePerSlot < 0)
            {
                return "Price per slot must greater or equal 0!";
            }

            return null;
        }
    }
}
