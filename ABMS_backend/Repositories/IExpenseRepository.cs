using ABMS_backend.DTO.ExpenseDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IExpenseRepository
    {
        ResponseData<Expense> createExpense(ExpenseInsert dto);
        ResponseData<string> updateExpense(string id, ExpenseInsert dto);
        ResponseData<string> deleteExpense(string id);
        ResponseData<Expense> getExpenseById(string id);
        ResponseData <List<Expense>> getAllExpense(ExpenseSearch dto);
        byte[] ExportData(string buildingId);
    }
}
