using ABMS_backend.DTO;
using ABMS_backend.Models;
using System.Net;
using ABMS_backend.Utils.Validates;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;

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

        #region Role resident
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
                f.Title = (float)dto.title;
                f.Content = (int)dto.content;
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

        #endregion


    }
}
