using ABMS_backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ABMS_backend.DTO
{
    public class RoomForInsertDTO
    {
       
        public string accountId { get; set; }
        public string buildingId { get; set; }
       
        public string roomNumber { get; set; }
       
        public float roomArea { get; set; }
       
        public int? numberOfResident { get; set; }
        

        
    }
}
