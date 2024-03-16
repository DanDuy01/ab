namespace ABMS_backend.DTO.VisitorDTO
{
    public class VisitorForSearchDTO
    {
        public string? roomId { get; set; }
        public string? building_id { get; set; }
        public string? fullName { get; set; }
        public DateTime? time { get; set; }
        public string? phoneNumber { get; set; }
        public int? status { get; set; }


    }
}
