namespace ABMS_backend.DTO.ExpenseDTO
{
    public class ExpenseSearch
    {
        public string? building_Id { get; set; }
        public float? money { get; set; }
        public string? expenseSource { get; set; }
        public string? description { get; set; }
        public string? createUser { get; set; }
        public DateTime? createTime { get; set; }
        public string? modifyUser { get; set; }
        public DateTime? modifyTime { get; set; }
        public int? status { get; set; }
    }
}
