using ABMS_backend.DTO;
using ABMS_backend.DTO.VisitorDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IVisitorManagementRepository
    {
        ResponseData<string> createRequestVisitor(VisitorForInsertDTO dto);
        
        ResponseData<string> deleteRequestVisitor(string id);
        ResponseData<string> updateRequestVisitor(string id, VisitorForInsertDTO dto);

        ResponseData<List<Visitor>> getAllRequestVisitor(VisitorForSearchDTO dto);

        ResponseData<Visitor> getRequestVisitorById(string id);
        ResponseData<string> manageVisitor(string id, int status);

    }
}
