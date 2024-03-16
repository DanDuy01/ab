using ABMS_backend.DTO;
using ABMS_backend.DTO.FeedbackDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Repositories
{
    public interface IFeedbackManagementRepository
    {
        ResponseData<string> createFeedback(FeedbackInsert dto);
        ResponseData<string> deleteFeedback(string id);
        ResponseData<string> updateFeedback(string id, FeedbackInsert dto);
        ResponseData<List<Feedback>> getAllFeedback(FeedbackForSearch dto);
        ResponseData<Feedback> getFeedbackById(string id);
    }
}
