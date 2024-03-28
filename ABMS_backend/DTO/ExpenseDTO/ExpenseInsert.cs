namespace ABMS_backend.DTO.ExpenseDTO
{
    public class ExpenseInsert
    {
        public string building_Id { get; set; }
        public float money { get; set; }
        public string? expenseSource { get; set; }
        public string? description { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int status { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(building_Id))
            {
                return "Building is required!";
            }
            else if (money == null && money < 0)
            {
                return "Money must be more than 0 and not null!";
            }

            return null;
        }
    }
}
