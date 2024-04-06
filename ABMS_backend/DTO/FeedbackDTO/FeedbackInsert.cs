using ABMS_backend.Models;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ABMS_backend.DTO.FeedbackDTO
{
    public class FeedbackInsert
    {
        public string room_Id { get; set; }
        public string serviceType_Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string? image { get; set; }
        public DateTime createTime { get; set; }
        public int status { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(room_Id))
            {
                return "Room is required!";
            }
            if (string.IsNullOrEmpty(serviceType_Id))
            {
                return "Service type is required!";
            }
            if (string.IsNullOrEmpty(title))
            {
                return "Title is required!";
            }
            if (string.IsNullOrEmpty(content))
            {
                return "Content is required!";
            }

            return null;
        }
    }
}
