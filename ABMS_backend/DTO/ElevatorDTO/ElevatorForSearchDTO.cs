﻿namespace ABMS_backend.DTO.ElevatorDTO
{
    public class ElevatorForSearchDTO
    {
        public string? room_id { get; set; }
        public string? building_id { get; set; }

        public DateTime? time { get; set; }

        public int? status { get; set; }
    }
}
