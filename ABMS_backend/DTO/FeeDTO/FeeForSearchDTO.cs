namespace ABMS_backend.DTO.FeeDTO
{
    public class FeeForSearchDTO
    {
        public string? feeName { get; set; }
        public string? buildingId { get; set; }
        public int? price { get; set; }
        public string? unit { get; set; }
        public DateOnly? effective_Date { get; set; }
    }
}
