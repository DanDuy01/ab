namespace ABMS_backend.DTO.AccountDTO
{
    public class AccountForSearchDTO
    {
        public string? searchMessage { get; set; }

        public string? buildingId { get; set; }

        public int? role { get; set; }

        public int? status { get; set; }
    }
}
