using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class ReservationForInsertDTO
    {
        public string RoomId { get; set; }

        public string UtilityId { get; set; }

        public int Slot { get; set; }

        public DateOnly BookingDate { get; set; }

        public int? NumberOfPerson { get; set; }

        public float TotalPrice { get; set; }

        public string? Description { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(RoomId))
            {
                return "Room is required!";
            }

            else if (String.IsNullOrEmpty(UtilityId))
            {
                return "Utility is required!";
            }

            else if (BookingDate == null)
            {
                return "Booking date is required!";
            }

            else if (BookingDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return "Invalid booking date!";
            }

            else if (NumberOfPerson <= 0)
            {
                return "Invalid number of person!";
            }

            else if (TotalPrice <= 0)
            {
                return "Invalid price!";
            }
            return null;
        }
    }
}
