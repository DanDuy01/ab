namespace ABMS_backend.DTO.FeedbackDTO
{
    public class FeedbackForSearch
    {
        public string? roomId {  get; set; }
        public string? serviceTypeId {  get; set; }
        public string? title { get; set; }
        public string? content {  get; set; }
        public string? image {  get; set; }
        public DateTime? createdTime { get; set; }
        public int? status { get; set; }
        public string? buildingId { get; set; }
    }
}
