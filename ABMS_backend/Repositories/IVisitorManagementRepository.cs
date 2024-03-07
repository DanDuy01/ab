using ABMS_backend.DTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IVisitorManagementRepository
    {
        ResponseData<string> createRequestVisitor(VisitorForInsertDTO dto);
        
        ResponseData<string> deleteRequestVisitor(string id);
       
        ResponseData<List<Visitor>> getAllRequestVisitor(VisitorForSearchDTO dto);

    }
}
