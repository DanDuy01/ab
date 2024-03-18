using ABMS_backend.Models;

namespace ABMS_backend.DTO.PostDTO
{
    public class PostForInsertDTO
    {
        public string title { get; set; }
        public string buildingId { get; set; }
        public string content { get; set; }
        public string image { get; set; }      
        public int type { get; set; }

        public string Validate()
        {           
            if (String.IsNullOrEmpty(buildingId))
            {
                return "Building is required!";
            }
            if (String.IsNullOrEmpty(title))
            {
                return "Title is required!";
            }
            if (String.IsNullOrEmpty(content))
            {
                return "Content is required!";
            }
           
            return null;
        }
    }
}
