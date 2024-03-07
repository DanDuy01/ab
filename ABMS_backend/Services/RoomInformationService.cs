using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace ABMS_backend.Services
{
    public class RoomInformationService : IRoomInformationRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomInformationService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createRoomInformation(RoomForInsertDTO dto)
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
                Room room = new Room();
                room.Id = Guid.NewGuid().ToString();
                room.AccountId = dto.accountId;
                room.BuildingId = dto.buildingId;
                room.RoomNumber = dto.roomNumber;
                room.RoomArea = dto.roomArea;
                room.NumberOfResident = dto.numberOfResident;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                room.CreateUser = getUser;
                room.CreateTime = DateTime.Now;
                room.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Rooms.Add(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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

        public ResponseData<string> deleteRoomInformation(string id)
        {
            try
            {
                Room room = _abmsContext.Rooms.Find(id);

                if (room == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                if (_httpContextAccessor.HttpContext.Session.GetString("user") == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        ErrMsg = ErrorApp.FORBIDDEN.description
                    };
                }
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                room.ModifyUser = getUser;
                room.ModifyTime = DateTime.Now;
                room.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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

        public ResponseData<List<Room>> getRoomInformation(RoomForSearchDTO dto)
        {
            var list = _abmsContext.Rooms.
                 Where(x => (dto.accountId == null || x.AccountId == dto.accountId)
                 && (dto.roomNumber == null || x.RoomNumber.ToLower()
                 .Contains(dto.roomNumber.ToLower()))).ToList();
            return new ResponseData<List<Room>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };

        }

        public ResponseData<Room> getRoomInformationById(string id)
        {
            Room room = _abmsContext.Rooms.Find(id);
            if (room == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Room>
            {
                Data = room,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateRoomInformation(string id, RoomForInsertDTO dto)
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
                Room room = _abmsContext.Rooms.Find(id);
               
                if (room == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                room.AccountId = dto.accountId;
                room.BuildingId = dto.buildingId;
                room.RoomNumber = dto.roomNumber;
                room.RoomArea = dto.roomArea;
                room.NumberOfResident = dto.numberOfResident;
                if (_httpContextAccessor.HttpContext.Session.GetString("user") == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        ErrMsg = ErrorApp.FORBIDDEN.description
                    };
                }
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                room.ModifyUser = getUser;
                room.ModifyTime = DateTime.Now;
                
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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
    }
}
