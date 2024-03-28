using ABMS_backend.DTO;
using ABMS_backend.DTO.ExpenseDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ABMS_backend.Services
{
    public class ExpenseService : IExpenseRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpenseService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<Expense> createExpense(ExpenseInsert dto)
        {
            //validate
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<Expense>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }

            try
            {
                Expense e = new Expense();
                e.Id = Guid.NewGuid().ToString();
                e.BuildingId = dto.building_Id;
                e.Expense1 = (float)dto.money;
                e.ExpenseSource = dto.expenseSource;
                e.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                e.CreateUser = getUser;
                e.CreateTime = DateTime.Now;
                e.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Expenses.Add(e);
                _abmsContext.SaveChanges();
                return new ResponseData<Expense>
                {
                    Data = e,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<Expense>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> updateExpense(string id, ExpenseInsert dto)
        {
            //validate
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }

            try
            {
                Expense e = _abmsContext.Expenses.Find(id);
                if (e == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Expense not found!"
                    };
                }

                e.BuildingId = dto.building_Id;
                e.Expense1 = dto.money;
                e.ExpenseSource = dto.expenseSource;
                e.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                e.ModifyUser = getUser;
                e.ModifyTime = DateTime.Now;
                _abmsContext.Expenses.Update(e);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = e.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Update failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteExpense(string id)
        {
            try
            {
                Expense e = _abmsContext.Expenses.Find(id);
                if (e == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Expense not found!"
                    };
                }

                _abmsContext.Expenses.Remove(e);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = e.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Delete failed: " + ex.Message
                };
            }
        }

        public ResponseData<Expense> getExpenseById(string id)
        {
            Expense e = _abmsContext.Expenses.Find(id);
            if (e == null)
            {
                return new ResponseData<Expense>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Expense not found!"
                };
            }

            return new ResponseData<Expense>
            {
                Data = e,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<List<Expense>> getAllExpense(ExpenseSearch dto)
        {
            var list = _abmsContext.Expenses.Include(x => x.Building)
                .Where(x => (dto.building_Id == null || x.BuildingId == dto.building_Id)
                    && (dto.money == null || x.Expense1 == dto.money)
                    && (dto.expenseSource == null || x.ExpenseSource.ToLower().Contains(dto.expenseSource.ToLower()))
                    && (dto.description == null || x.Description.ToLower().Contains(dto.description.ToLower()))
                    && (dto.createUser == null || x.CreateUser.ToLower().Contains(dto.createUser.ToLower()))
                    && (dto.createTime == null || x.CreateTime == dto.createTime)
                    && (dto.modifyTime == null || x.ModifyUser.ToLower().Contains(dto.modifyUser.ToLower()))
                    && (dto.modifyTime == null || x.ModifyTime == dto.modifyTime)
                    && (dto.status == null || x.Status == dto.status))
                .Select(x => new Expense
                {
                    Id = x.Id,
                    BuildingId = x.BuildingId,
                    Expense1 = x.Expense1,
                    ExpenseSource = x.ExpenseSource,
                    Description = x.Description,
                    CreateUser = x.CreateUser,
                    CreateTime = x.CreateTime,
                    ModifyUser = x.ModifyUser,
                    ModifyTime = x.ModifyTime,
                    Status = x.Status
                }).ToList();

            return new ResponseData<List<Expense>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }
    }
}
