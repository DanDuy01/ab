namespace ABMS_backend.DTO.ServiceChargeDTO
{
    public class ServiceChargeForSearchDTO
    {
        public string? building_id { get; set; }

        public string? room_id { get; set; }

        public int? month { get; set; }

        public int? year { get; set; }
    }
}
