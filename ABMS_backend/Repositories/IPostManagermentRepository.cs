using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.DTO.PostDTO;

namespace ABMS_backend.Repositories
{
    public interface IPostManagermentRepository
    {
        ResponseData<string> createPostForResident(PostForInsertDTO dto);
        ResponseData<string> createPostForReceptionist(PostForInsertDTO dto);
            
        ResponseData<string> deletePost(string id);
        ResponseData<string> updatePost(string id, PostForInsertDTO dto);
        ResponseData<List<Post>> getAllPost(PostForSearchDTO dto);
        ResponseData<Post> getPostById(string id);
        IEnumerable<PostNotificationDTO> GetNotifications(string accountId, int skip, int take);
        public void MarkNotificationsAsRead(string accountId);
    }
}
