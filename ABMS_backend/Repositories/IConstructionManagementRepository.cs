using ABMS_backend.DTO;
using ABMS_backend.DTO.ConstructionDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IConstructionManagementRepository
    {
        ResponseData<string> createConstruction(ConstructionInsertDTO dto);
        ResponseData<string> deleteConstruction(string id);
        ResponseData<string> updateConstruction(string id, ConstructionInsertDTO dto);
        ResponseData<List<Construction>> getAllConstruction(ConstructionForSearchDTO dto);
 
        ResponseData<string> manageConstruction(string id, ConstructionForManageDTO dto);

        ResponseData<Construction> getContructionById(string id);
    }
}
