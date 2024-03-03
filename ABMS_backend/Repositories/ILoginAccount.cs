using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface ILoginAccount
    {
        ResponseData<string> getAccount(Login dto);
        ResponseData<string> getAccountByEmail(LoginWithEmail dto);
        ResponseData<string> Register(dtotest request);
    }
}
