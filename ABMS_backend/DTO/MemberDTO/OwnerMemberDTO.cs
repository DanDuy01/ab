namespace ABMS_backend.DTO.MemberDTO
{
    public class OwnerMemberDTO
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsHouseholder { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; } = null!;
        public int Status { get; set; }
    }
}
