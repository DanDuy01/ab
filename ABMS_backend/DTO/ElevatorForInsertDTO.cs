namespace ABMS_backend.DTO
{
    public class ElevatorForInsertDTO
    {
        public string room_id { get; set; }

        public DateTime start_time {  get; set; }

        public DateTime end_time { get; set;}

        public string? description { get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(room_id))
            {
                return "Room is required!";
            }

            if(start_time < DateTime.Now)
            {
                return "Invalid start time!";
            }
            
            if(end_time < start_time)
            {
                return "End time must after start time!";
            }

            return null;
        }
    }
}
