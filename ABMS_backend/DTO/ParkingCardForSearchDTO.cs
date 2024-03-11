namespace ABMS_backend.DTO
{
    public class ParkingCardForSearchDTO
    {
        public string? resident_id { get; set; }

        public string? room_id { get; set; }

        public DateOnly? expire_date { get; set; }

        public int? status { get; set; }

    }
}
