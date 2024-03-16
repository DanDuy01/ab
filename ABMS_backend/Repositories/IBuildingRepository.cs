using ABMS_backend.DTO;
using ABMS_backend.DTO.BuildingDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IBuildingRepository
    {
        ResponseData<string> createBuilding(BuildingForInsertDTO dto);

        ResponseData<string> updateBuilding(string id, BuildingForInsertDTO dto);

        ResponseData<string> deleteBuilding(string id);

        ResponseData<List<Building>> getBuilding(BuildingForSearchDTO dto);

        ResponseData<Building> getBuildingById(string id);
    }
}
