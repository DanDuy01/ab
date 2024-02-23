using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IResidentAccountManagementRepository
    {

        ResponseData<string> createResidentAccount(ResidentForInsertDTO dto);
        ResponseData<string> updateResidentAccount(string id, ResidentForInsertDTO dto);
        ResponseData<string> deleteResidentAccount(string id);
        ResponseData<Resident> getResidentById(string id);
        ResponseData<List<Resident>> getResidentAccount(ResidentForSearchDTO dto);
    }
}
