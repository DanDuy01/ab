using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.Net;

namespace ABMS_backend.Services
{
    public class ConstructionServices : IConstructionManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConstructionServices(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Role resident
        public ResponseData<string> createConstruction(ConstructionInsertDTO dto)
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
                Construction c = new Construction();
                c.Id = Guid.NewGuid().ToString();
                c.RoomId = dto.roomId;
                c.Name = dto.name;
                c.ConstructionOrganization = dto.constructionOrganization;
                c.PhoneContact = dto.phone;
                c.StartTime = dto.startTime;
                c.EndTime = dto.endTime;
                c.Description = dto.description;
                c.CreateTime = DateTime.Now;
                c.ApproveUser = dto.approveUser;
                c.Status = (int)Constants.STATUS.SENT;
                _abmsContext.Constructions.Add(c);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = c.Id,
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

        public ResponseData<string> deleteConstruction(string id)
        {
            try
            {
                Construction c = _abmsContext.Constructions.Find(id);
                if (c == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Invalid construction!"
                    };
                }

                c.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.Constructions.Update(c);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = c.Id,
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

        public ResponseData<List<Construction>> getConstruction(ConstructionForSearchDTO dto)
        {
            var list = _abmsContext.Constructions.
                Where(x =>
                    (dto.roomId == null || x.RoomId == dto.roomId)
                    && (dto.name == null || x.Name == dto.name)
                    && (dto.constructionOrganization == null || x.ConstructionOrganization == dto.constructionOrganization)
                    && (dto.phone == null || x.PhoneContact == dto.phone)
                    && (dto.time == null || (x.StartTime <= dto.time && dto.time <= x.EndTime))
                    && (dto.status == null || x.Status == dto.status))
                .ToList();
            return new ResponseData<List<Construction>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Construction> getContructionById(string id)
        {
            Construction c = _abmsContext.Constructions.Find(id);
            if (c == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Construction>
            {
                Data = c,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        #endregion

        #region Role reception
        public ResponseData<string> manageConstruction(string id, int status)
        {
            Construction c = _abmsContext.Constructions.Find(id);
            if (c == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            c.Status = status;
            if (_httpContextAccessor.HttpContext.Session.GetString("user") == null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ErrMsg = ErrorApp.FORBIDDEN.description
                };
            }
            c.ApproveUser = _httpContextAccessor.HttpContext.Session.GetString("user");
            _abmsContext.Constructions.Update(c);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = c.Id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        #endregion
    }
}
