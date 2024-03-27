using ABMS_backend.DTO;
using ABMS_backend.DTO.ServiceTypeDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IServiceTypeRepository
    {
        ResponseData<ServiceType> createServiceType(ServiceTypeInsert dto);
        ResponseData<string> updateServiceType(string id, ServiceTypeInsert dto);
        ResponseData<string> deleteServiceType(string id);
        ResponseData<ServiceType> getServiceTypeById(string id);
        ResponseData<List<ServiceType>> getAllServiceType(ServiceTypeSearch dto);
    }
}
