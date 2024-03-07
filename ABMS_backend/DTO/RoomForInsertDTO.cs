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
        
        public string Validate()
        {
            if (String.IsNullOrEmpty(accountId))
            {
                return "AccountId is required!";
            }
            if (String.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }
            if (String.IsNullOrEmpty(roomNumber))
            {
                return "Room number is required!";
            }
            if (roomArea < 0)
            {
                return "Invalid room area!";
            }
            if (numberOfResident < 0)
            {
                return "Invalid number of resident!";
            }

            return null;
        }


    }
}
