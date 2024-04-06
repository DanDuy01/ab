using ABMS_backend.DTO;
using ABMS_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_backend.Repositories
{
    public interface ILoginAccount
    {
        ResponseData<string> GetAccount(Login dto);
        ResponseData<string> GetAccountByEmail(LoginWithEmail dto);
        ResponseData<string> ChangePassword(string id, ChangePassword password);
        ResponseData<string> ResetPassword(string id);
        ResponseData<string> Register(RegisterDTO request);
        ResponseData<string> ImportData(IFormFile file, int role,string buildingId);
        byte[] ExportData(string buildingId);

        public ResponseData<string> SendOtp(string id);
        public ResponseData<string> VerifyOtpAndResetPassword(string id, string otp);
    }
}
