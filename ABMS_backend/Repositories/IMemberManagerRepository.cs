using ABMS_backend.DTO;
using ABMS_backend.DTO.MemberDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IMemberManagerRepository
    {
        ResponseData<string> createMember(MemberForInsertDTO dto);
        ResponseData<string> updateMember(string id, MemberForInsertDTO dto);
        ResponseData<string> deleteMember(string id);
        ResponseData<Resident> getMemberById(string id);
        ResponseData<OwnerMemberDTO> GetHouseOwnerByRoomId(string roomId);
        ResponseData<List<Resident>> getAllMember(MemberForSearchDTO dto);
    }
}
