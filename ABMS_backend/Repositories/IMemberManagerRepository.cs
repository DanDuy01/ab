using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IMemberManagerRepository
    {
        ResponseData<string> createMember(MemberForInsertDTO dto);
        ResponseData<string> updateMember(string id, MemberForInsertDTO dto);
        ResponseData<string> deleteMember(string id);
        ResponseData<Resident> getMemberById(string id);
        ResponseData<List<Resident>> getAllMember(MemberForInsertDTO dto);
    }
}
