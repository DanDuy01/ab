using ABMS_backend.DTO;
using ABMS_backend.Models;
using System.Net;
using ABMS_backend.Utils.Validates;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.DTO.FeedbackDTO;
using Microsoft.EntityFrameworkCore;
using ABMS_backend.DTO.VisitorDTO;

namespace ABMS_backend.Services
{
    public class FeedbackService : IFeedbackManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createFeedback(FeedbackInsert dto)
        {
            //validate
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
                Feedback f = new Feedback();
                f.Id = Guid.NewGuid().ToString();
                f.RoomId = dto.room_Id;
                f.ServiceTypeId = dto.serviceType_Id;
                f.Title = dto.title;
                f.Content = dto.content;
                f.Image = dto.image;
                f.CreateTime = DateTime.Now;
                f.Status = (int)Constants.STATUS.SENT;
                _abmsContext.Feedbacks.Add(f);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = f.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteFeedback(string id)
        {
            try
            {
                Feedback f = _abmsContext.Feedbacks.Find(id);
                if (f == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Invalid feedback!"
                    };
                }

                f.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.Feedbacks.Update(f);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = f.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Delete failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> updateFeedback(string id, FeedbackInsert dto)
        {
            //validate
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
                Feedback f = _abmsContext.Feedbacks.Find(id);
                if (f == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Feedback not found!"
                    };
                }

                f.RoomId = dto.room_Id;
                f.ServiceTypeId = dto.serviceType_Id;
                f.Title = dto.title;
                f.Content = dto.content;
                f.Image = dto.image;
                f.CreateTime = dto.createTime;
                f.Status = dto.status;
                _abmsContext.Feedbacks.Update(f);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = f.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Update failed: " + ex.Message
                };
            }
        }

        public ResponseData<List<Feedback>> getAllFeedback(FeedbackForSearch dto)
        {
            var list = _abmsContext.Feedbacks.Include(x => x.Room).Include(x => x.ServiceType)
                .Where(x => (dto.roomId == null || x.RoomId == dto.roomId)
                && (dto.serviceTypeId == null || x.ServiceTypeId == dto.serviceTypeId)
                && (dto.title == null || x.Title == dto.title)
                && (dto.content == null || x.Content == dto.content)
                && (dto.image == null || x.Image == dto.image)
                && (dto.createdTime == null || x.CreateTime == dto.createdTime)
                && (dto.status == null || x.Status == dto.status)).Select(x => new Feedback
                {
                    Id = x.Id,
                    RoomId = x.RoomId,
                    ServiceTypeId = x.ServiceTypeId,
                    Title = x.Title,
                    Content = x.Content,
                    Image = x.Image,
                    CreateTime = x.CreateTime,
                    Status = x.Status
                }).ToList();

            return new ResponseData<List<Feedback>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Feedback> getFeedbackById(string id)
        {
            Feedback f = _abmsContext.Feedbacks.Find(id);
            if (f == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }

            return new ResponseData<Feedback>
            {
                Data = f,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
