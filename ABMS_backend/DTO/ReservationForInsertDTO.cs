using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class ReservationForInsertDTO
    {
        public string room_id { get; set; }

        public string utility_detail_id { get; set; }

        public string slot { get; set; }

        public DateOnly booking_date { get; set; }

        public int? number_of_person { get; set; }

        public float total_price { get; set; }

        public string? description { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(room_id))
            {
                return "Room is required!";
            }

            else if (String.IsNullOrEmpty(utility_detail_id))
            {
                return "Utility Detail is required!";
            }

            else if (booking_date == null)
            {
                return "Booking date is required!";
            }

            else if (booking_date < DateOnly.FromDateTime(DateTime.Now))
            {
                return "Invalid booking date!";
            }

            else if (number_of_person <= 0)
            {
                return "Invalid number of person!";
            }

            else if (total_price <= 0)
            {
                return "Invalid price!";
            }
            return null;
        }
    }
}
