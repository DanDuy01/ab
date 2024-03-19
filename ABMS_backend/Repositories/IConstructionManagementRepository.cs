using ABMS_backend.DTO;
using ABMS_backend.DTO.ConstructionDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IConstructionManagementRepository
    {
        ResponseData<string> createConstruction(ConstructionInsertDTO dto);
        ResponseData<string> deleteConstruction(string id);
<<<<<<< HEAD
        ResponseData<string> updateConstruction(string id, ConstructionInsertDTO dto);
        ResponseData<List<Construction>> getAllConstruction(ConstructionForSearchDTO dto);
        ResponseData<string> manageConstruction(string id, int status);
=======
        ResponseData<List<Construction>> getConstruction(ConstructionForSearchDTO dto);
        ResponseData<string> manageConstruction(string id, ConstructionForManageDTO dto);
>>>>>>> deploy
        ResponseData<Construction> getContructionById(string id);
    }
}
