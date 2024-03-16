using ABMS_backend.DTO;

namespace ABMS_backend.Repositories
{
    public interface IFeedbackManagementRepository
    {
        ResponseData<string> createFeedback(FeedbackInsert dto);
        ResponseData<string> deleteFeedback(string id);
    }
}
