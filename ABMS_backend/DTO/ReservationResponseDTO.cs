namespace ABMS_backend.DTO
{
    public class ReservationResponseDTO
    {
        public string room_id { get; set; }

        public string utility_id { get; set;}

        public int slot { get; set; }

        public DateOnly booking_date { get; set; }

        public int? number_of_person { get; set; }
        
        public float total_price { get; set; }

        public string? description { get; set; }

        public string? approve_user { get; set; }

        public int status { get; set; }
    }
}
