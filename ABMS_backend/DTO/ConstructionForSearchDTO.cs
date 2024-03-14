using System.ComponentModel.DataAnnotations;

namespace ABMS_backend.DTO
{
    public class ConstructionForSearchDTO
    {
        public string? roomId { get; set; }
        public string? name { get; set; }
        public string? building_id { get; set; }
        public string? constructionOrganization { get; set; }
        [Phone]
        public string? phone { get; set; }


        public DateTime? time { get; set; }
        public int? status { get; set; }
    }
}
