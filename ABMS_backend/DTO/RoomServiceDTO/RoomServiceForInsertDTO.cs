using System;

namespace ABMS_backend.DTO.RoomServiceDTO
{
    public class RoomServiceForInsertDTO
    {
        public string room_id {  get; set; }

        public string fee_id { get; set; }

        public int amount { get; set; }

        public string? description { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(room_id))
            {
                return "Room is required!";
            }

            else if (string.IsNullOrEmpty(fee_id))
            {
                return "Service is required!";
            }

            else if (amount < 1)
            {
                return "Wrong amount!";
            }
            return null;
        }
    }
}
