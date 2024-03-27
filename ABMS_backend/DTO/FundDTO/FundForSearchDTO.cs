namespace ABMS_backend.DTO.FundDTO
{
    public class FundForSearchDTO
    {
        public string? id { get; set; }
        public string? buildingId { get; set; }
        public float? fund { get; set; }
        public string? fundSource { get; set; }
        public int? status { get; set; }
    }
}
