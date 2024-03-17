using ABMS_backend.DTO;
using ABMS_backend.DTO.ConstructionDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IConstructionManagementRepository
    {
        ResponseData<string> createConstruction(ConstructionInsertDTO dto);
        ResponseData<string> deleteConstruction(string id);
        ResponseData<List<Construction>> getConstruction(ConstructionForSearchDTO dto);
        ResponseData<string> manageConstruction(string id, int status);
        ResponseData<Construction> getContructionById(string id);
    }
}
