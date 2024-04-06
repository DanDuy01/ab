namespace ABMS_backend.DTO.ServiceChargeDTO
{
    public class ServiceChargeListResponseDTO
    {
        public string id { get; set; }

        public string room_id { get; set; }

        public string room_number {  get; set; }

        public int status { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public float total_price { get; set; }

        public string? description { get; set; }
    }
}
