using ABMS_backend.DTO.RoomServiceDTO;
using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using System.Net;

namespace ABMS_backend.Services
{
    public class RoomServiceService : IRoomServiceRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomServiceService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createRoomService(RoomServiceForInsertDTO dto)
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
                RoomService rs = _abmsContext.RoomServices.FirstOrDefault
                    (x => x.RoomId == dto.room_id && x.FeeId == dto.fee_id);
                if (rs != null)
                {
                    rs.Amount = dto.amount;
                    _abmsContext.RoomServices.Update(rs);
                    _abmsContext.SaveChanges();
                    return new ResponseData<string>
                    {
                        Data = rs.Id,
                        StatusCode = HttpStatusCode.OK,
                        ErrMsg = ErrorApp.SUCCESS.description
                    };
                }
                RoomService roomService = new RoomService();
                roomService.Id = Guid.NewGuid().ToString();
                roomService.RoomId = dto.room_id;
                roomService.FeeId = dto.fee_id;
                roomService.Amount = dto.amount;
                roomService.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                roomService.CreateUser = getUser;
                roomService.CreateTime = DateTime.Now;
                roomService.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.RoomServices.Add(roomService);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = roomService.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteRoomService(string id)
        {
            try
            {
                RoomService roomService = _abmsContext.RoomServices.Find(id);
                if (roomService == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                roomService.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.RoomServices.Update(roomService);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = roomService.Id,
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

        public ResponseData<string> deleteDraftRoomService(List<string> idList)
        {
            try
            {
                foreach (String id in idList)
                {
                    RoomService roomService = _abmsContext.RoomServices.Find(id);
                    if (roomService == null)
                    {
                        throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                    }
                    _abmsContext.RoomServices.Remove(roomService);
                    _abmsContext.SaveChanges();
                }
                return new ResponseData<string>
                {
                    Data = idList.ToString(),
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

        public ResponseData<List<RoomService>> getRoomService(RoomServiceForSearchDTO dto)
        {
            var list = _abmsContext.RoomServices.Where(x =>
            (dto.room_id == null || x.RoomId == dto.room_id)
                && (dto.fee_id == null || x.FeeId == dto.fee_id)
                && (dto.status == null || x.Status == dto.status)).ToList();
            return new ResponseData<List<RoomService>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<RoomService> getRoomServiceById(string id)
        {
            RoomService roomService = _abmsContext.RoomServices.Find(id);
            if (roomService == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<RoomService>
            {
                Data = roomService,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
