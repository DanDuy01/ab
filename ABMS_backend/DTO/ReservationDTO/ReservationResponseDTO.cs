﻿using ABMS_backend.Models;

namespace ABMS_backend.DTO.ReservationDTO
{
    public class ReservationResponseDTO
    {
        public string id { get; set; }
        public string room_id { get; set; }

        public string utility { get; set; }

        public string utility_detail_id { get; set; }

        public string utility_detail_name { get; set; }

        public string slot { get; set; }

        public DateOnly booking_date { get; set; }

        public int? number_of_person { get; set; }

        public float total_price { get; set; }

        public string? description { get; set; }

        public string? approve_user { get; set; }

        public int status { get; set; }

        public string room_number { get; set; }
    }
}
