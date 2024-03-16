using ABMS_backend.Models;

namespace ABMS_backend.DTO
{
    public class RoomBuildingResponseDTO
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string BuildingId { get; set; }
        public string RoomNumber { get; set; }
        public float RoomArea { get; set; }
        public int? NumberOfResident { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string? ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }

        public  ICollection<Resident> Residents { get; set; }
        public int Status { get; set; }
        // Additional properties from Building
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }
    }
}
