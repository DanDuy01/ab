namespace ABMS_backend.DTO
{
    public class ResevationForResidentSearchDTO
    {
        public string? roomId { get; set; }

        public string? utilityId { get; set; }

        public string? utilityDetailId { get; set; }

        public DateOnly? bookingDate { get; set; }

    }
}
