using ABMS_backend.DTO;
using ABMS_backend.DTO.ConstructionDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
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

        public ResponseData<string> updateConstruction(string id, ConstructionInsertDTO dto)
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
                Construction c = _abmsContext.Constructions.Find(id);
                if (c == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Construction not found!"
                    };
                }

                c.RoomId = dto.roomId;
                c.Name = dto.name;
                c.ConstructionOrganization = dto.constructionOrganization;
                c.PhoneContact = dto.phone;
                c.StartTime = dto.startTime;
                c.EndTime = dto.endTime;
                c.Description = dto.description;
                c.CreateTime = DateTime.Now;
                c.Status = (int)Constants.STATUS.SENT;
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
                    ErrMsg = "Update failed: " + ex.Message
                };
            }
        }

        public ResponseData<List<Construction>> getAllConstruction(ConstructionForSearchDTO dto)
        {
            var list = _abmsContext.Constructions.Include(x => x.Room)
                .Where(x => (dto.roomId == null || x.RoomId == dto.roomId)
                    && (dto.name == null || x.Name.ToLower().Contains(dto.name.ToLower()))
                    && (dto.building_id == null || x.Room.BuildingId == dto.building_id)
                    && (dto.constructionOrganization == null || x.ConstructionOrganization.ToLower().Contains(dto.constructionOrganization.ToLower()))
                    && (dto.phone == null || x.PhoneContact.Contains(dto.phone))
                    && (dto.time == null || (x.StartTime <= dto.time && dto.time <= x.EndTime))
                    && (dto.status == null || x.Status == dto.status)).Select(x => new Construction
                    {
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        Name = x.Name,
                        ConstructionOrganization = x.ConstructionOrganization,
                        PhoneContact = x.PhoneContact,
                        Description = x.Description,
                        CreateTime = x.CreateTime,
                        Status = x.Status,
                        Id = x.Id,
                        RoomId = x.RoomId,
                        Room = x.Room,
                        ApproveUser = x.ApproveUser
                    })
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
            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            c.ApproveUser = getUser;
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
