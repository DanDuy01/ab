using ABMS_backend.DTO.PostDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.DTO.HotlineDTO;

namespace ABMS_backend.Repositories
{
    public interface IHotlineManagementRepository
    {
        ResponseData<string> createHotline(HotlineForInsertDTO dto);
        ResponseData<string> deleteHotline(string id);
        ResponseData<string> updateHotline(string id, HotlineForInsertDTO dto);
        ResponseData<List<Hotline>> getAllHotline(HotlineForSearchDTO dto);
        ResponseData<Hotline> getHotlineById(string id);
    }
}
