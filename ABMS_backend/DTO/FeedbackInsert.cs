using ABMS_backend.Models;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO
{
    public class FeedbackInsert
    {
        public string room_Id {  get; set; }
        public string serviceType_Id {  get; set; }
        public float title { get; set; }
        public int content {  get; set; }
        public string? image {  get; set; }
        public DateTime createTime { get; set; }
        public int status {  get; set; }

        public string Validate()
        {
            if (String.IsNullOrEmpty(room_Id))
            {
                return "Room is required!";
            }

            if (String.IsNullOrEmpty(serviceType_Id))
            {
                return "Service type is required!";
            }

            return null;
        }
    }
}
