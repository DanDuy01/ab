using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                    ErrMsg = "Delete failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<RoomBuildingResponseDTO>> getRoomInformation(RoomForSearchDTO dto)
        {
            var list = _abmsContext.Rooms.Include(x => x.Residents).Include(x => x.Building)
                 .Where(x => (dto.accountId == null || x.AccountId == dto.accountId)
                 && (dto.buildingId == null || x.BuildingId == dto.buildingId)
                 && (dto.roomNumber == null || x.RoomNumber.ToLower().Contains(dto.roomNumber.ToLower())))
                 .Select(x => new RoomBuildingResponseDTO
                 {
                     // Map Room properties
                     Id = x.Id,
                     AccountId = x.AccountId,
                     BuildingId = x.BuildingId,
                     RoomNumber = x.RoomNumber,
                     RoomArea = x.RoomArea,
                     NumberOfResident = x.NumberOfResident,
                     CreateUser = x.CreateUser,
                     CreateTime = x.CreateTime,
                     ModifyUser = x.ModifyUser,
                     ModifyTime = x.ModifyTime,
                     Status = x.Status,
                     Residents = x.Residents,
                     BuildingName = x.Building.Name,
                     BuildingAddress = x.Building.Address
                 }).ToList();

            return new ResponseData<List<RoomBuildingResponseDTO>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = "Success", // Assuming ErrorApp.SUCCESS.description is "Success"
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
