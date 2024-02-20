using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Net;

namespace ABMS_backend.Services
{
    public class CmbAccountManagementService : ICmbAccountManagementService
    { 
        private readonly abmsContext _abmsContext;

        private IMapper _mapper;

        public CmbAccountManagementService(abmsContext abmsContext, IMapper mapper)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
        }

        ResponseData<string> ICmbAccountManagementService.createCmbAccount(CmbAccountForInsertDTO dto)
        {
            Account account = new Account();
            account = _mapper.Map<Account>(dto);
            account.Id = Guid.NewGuid().ToString();
            account.CreateUser = "admin";
            account.CreateTime = DateTime.Now;
            account.Status = (int)Constants.STATUS.ACTIVE;
            _abmsContext.Accounts.Add(account);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = account.Id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        ResponseData<string> ICmbAccountManagementService.deleteCmbAccount(string id)
        {
            throw new NotImplementedException();
        }

        List<ResponseData<Account>> ICmbAccountManagementService.getCmbAccount(CmbAccountForSearchDTO dto)
        {
            throw new NotImplementedException();
        }

        ResponseData<Account> ICmbAccountManagementService.getCmbAccountById(string id)
        {
            throw new NotImplementedException();
        }

        ResponseData<string> ICmbAccountManagementService.updateCmbAccount(string id, CmbAccountForInsertDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
