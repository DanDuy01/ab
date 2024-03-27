using ABMS_backend.DTO;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ABMS_backend.Services
{
    public class PostManagementService : IPostManagermentRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostManagementService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData<string> createPostForResident(PostForInsertDTO dto)
        {
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            try
            {
                // Create and save the new post
                Post post = new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = dto.title,
                    BuildingId = dto.buildingId,
                    Content = dto.content,
                    Image = dto.image,
                    Type = dto.type,
                    CreateUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]),
                    CreateTime = DateTime.Now,
                    Status = (int)Constants.STATUS.ACTIVE
                };
                _abmsContext.Posts.Add(post);
                _abmsContext.SaveChanges();

                var targetAccounts = _abmsContext.Accounts.Where(a => a.Role == 3 &&a.BuildingId == dto.buildingId).ToList();

                foreach (var account in targetAccounts)
                {
                    var accountPost = new AccountPost
                    {
                        Id = Guid.NewGuid().ToString(),
                        AccountId = account.Id,
                        PostId = post.Id,
                        IsRead = 0 // False, indicating unread
                    };
                    _abmsContext.AccountPosts.Add(accountPost);
                }

                // Save AccountPost entries
                _abmsContext.SaveChanges();

                // Return success response
                return new ResponseData<string>
                {
                    Data = post.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create failed why " + ex.Message
                };
            }
        }
        public ResponseData<string> createPostForReceptionist(PostForInsertDTO dto)
        {
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            try
            {
                // Create and save the new post
                Post post = new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = dto.title,
                    BuildingId = dto.buildingId,
                    Content = dto.content,
                    Image = dto.image,
                    Type = dto.type,
                    CreateUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]),
                    CreateTime = DateTime.Now,
                    Status = (int)Constants.STATUS.ACTIVE
                };
                _abmsContext.Posts.Add(post);
                _abmsContext.SaveChanges();

                var targetAccounts = _abmsContext.Accounts.Where(a => a.Role == 2 && a.BuildingId==dto.buildingId).ToList();

                foreach (var account in targetAccounts)
                {
                    var accountPost = new AccountPost
                    {
                        Id = Guid.NewGuid().ToString(), 
                        AccountId = account.Id,
                        PostId = post.Id,
                        IsRead = 0 
                    };
                    _abmsContext.AccountPosts.Add(accountPost);
                }

                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = post.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create failed why " + ex.Message
                };
            }
        }

        public ResponseData<string> deletePost(string id)
        {
            try
            {
                Post post = _abmsContext.Posts.Find(id);
                if (post == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                post.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                post.ModifyUser = getUser;
                post.ModifyTime = DateTime.Now;
                _abmsContext.Posts.Update(post);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = post.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };

            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Deleted failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Post>> getAllPost(PostForSearchDTO dto)
        {
            var list = _abmsContext.Posts.Where(x => (dto.id == null || x.Id == dto.id)
            && (dto.buildingId == null || x.BuildingId == dto.buildingId)
            && (dto.title == null || x.Title == dto.title)
            && (dto.type == null || x.Type == dto.type)).ToList();
            return new ResponseData<List<Post>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Post> getPostById(string id)
        {
            Post post = _abmsContext.Posts.Find(id);
            if (post == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Post>
            {
                Data = post,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updatePost(string id, PostForInsertDTO dto)
        {
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            try
            {
                Post post = _abmsContext.Posts.Find(id);
                if (post == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                post.Title = dto.title;
                post.BuildingId = dto.buildingId;
                post.Content = dto.content;
                post.Image = dto.image;
                post.Type = dto.type;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                post.ModifyUser = getUser;
                post.ModifyTime = DateTime.Now;
                _abmsContext.Posts.Update(post);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = post.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Update failed why " + ex.Message
                };
            }
        }
        public IEnumerable<PostNotificationDTO> GetNotifications(string accountId, int skip, int take)
        {
            var notifications = _abmsContext.AccountPosts
        .Where(ap => ap.AccountId == accountId && ap.Post.Type == 7) // Filter by account ID and post type
        .OrderByDescending(ap => ap.Post.CreateTime)
        .Select(ap => new PostNotificationDTO
        {
            Post = ap.Post,
            IsRead = ap.IsRead == 1 // Assuming IsRead is stored as an int (1 for true, 0 for false)
        })
        .Skip(skip)
        .Take(take)
        .ToList();

            return notifications;
        }
        public void MarkNotificationsAsRead(string accountId)
        {
            var unreadNotifications = _abmsContext.AccountPosts
                .Where(ap => ap.AccountId == accountId && ap.IsRead == 0)
                .ToList();

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = 1;
            }

            _abmsContext.SaveChanges();
        }

    }

}
