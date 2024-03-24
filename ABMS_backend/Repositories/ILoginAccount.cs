using ABMS_backend.DTO;
using ABMS_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Repositories
{
    public interface ILoginAccount
    {
        ResponseData<string> getAccount(Login dto);
        ResponseData<string> getAccountByEmail(LoginWithEmail dto);
        ResponseData<string> Register(RegisterDTO request);
        ResponseData<string> ImportData(IFormFile file, int role,string buildingId);
        byte[] ExportData(string buildingId);
    }
}
