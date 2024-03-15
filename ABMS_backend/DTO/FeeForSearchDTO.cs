namespace ABMS_backend.DTO
{
    public class FeeForSearchDTO
    {
        public string feeName { get; set; }
        public int price { get; set; }
        public string unit { get; set; }
        public DateOnly effective_Date { get; set; }
    }
}
