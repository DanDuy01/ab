namespace ABMS_backend.DTO
{
    public class ResevationForResidentSearchDTO
    {
        public string? roomId { get; set; }

        public string? utilityId { get; set; }

        public DateOnly? bookingDate { get; set; }

    }
}
