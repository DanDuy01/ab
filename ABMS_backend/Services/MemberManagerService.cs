using ABMS_backend.DTO;
using ABMS_backend.DTO.MemberDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class MemberManagerService : IMemberManagerRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public MemberManagerService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createMember(MemberForInsertDTO dto)
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
                bool householderExists = _abmsContext.Residents.Any(r => r.RoomId == dto.roomId && r.IsHouseholder && dto.isHouseHolder );

                if (householderExists)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = "Cannot create resident: A householder already exists in the specified room."
                    };
                }
                Resident resident = new Resident();
                resident.Id = Guid.NewGuid().ToString();
                resident.RoomId = dto.roomId;
                resident.FullName = dto.fullName;
                resident.DateOfBirth = dto.dob;
                resident.Gender = dto.gender;
                resident.Phone= dto.phone;
                resident.IsHouseholder = dto.isHouseHolder;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                resident.CreateUser = getUser;
                resident.CreateTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Residents.Add(resident);
                _abmsContext.SaveChanges();
                Room room = _abmsContext.Rooms.Find(dto.roomId);
                room.NumberOfResident++;
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = resident.Id,
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

        public ResponseData<string> deleteMember(string id)
        {
            try
            {
                Resident resident = _abmsContext.Residents.Find(id);
                if (resident == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                resident.ModifyUser = getUser;
                resident.ModifyTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.Residents.Update(resident);
                Room room = _abmsContext.Rooms.Find(resident.RoomId);
                room.NumberOfResident--;
                room.ModifyUser = getUser;
                room.ModifyTime = DateTime.Now;
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = resident.Id,
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

        public ResponseData<List<Resident>> getAllMember(MemberForSearchDTO dto)
        {
            var list = _abmsContext.Residents.Where(x =>(dto.roomId == null || x.RoomId == dto.roomId)).ToList();
            return new ResponseData<List<Resident>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Resident> getMemberById(string id)
        {
            Resident resident = _abmsContext.Residents.Find(id);
            if (resident == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Resident>
            {
                Data = resident,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateMember(string id, MemberForInsertDTO dto)
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
                Resident resident = _abmsContext.Residents.Find(id);
                if (resident == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                resident.RoomId = dto.roomId;
                resident.FullName = dto.fullName;
                resident.DateOfBirth = dto.dob;
                resident.Gender = dto.gender;
                resident.Phone= dto.phone;
                resident.IsHouseholder = dto.isHouseHolder;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                resident.ModifyUser = getUser;
                resident.ModifyTime = DateTime.Now;
                _abmsContext.Residents.Update(resident);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = resident.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Updated failed why " + ex.Message
                };
            }
        }

        public ResponseData<OwnerMemberDTO> GetHouseOwnerByRoomId(string roomId)
        {
            var houseOwner = _abmsContext.Residents
                .Include(r => r.Room)
                .Where(r => r.RoomId == roomId && r.IsHouseholder)
                .Select(r => new OwnerMemberDTO
                {
                    Id = r.Id,
                    RoomId = r.RoomId,
                    FullName = r.FullName,
                    DateOfBirth = r.DateOfBirth,
                    IsHouseholder = r.IsHouseholder,
                    Status=r.Status,
                    Phone=r.Phone,
                    Gender = r.Gender
                })
                .FirstOrDefault();

            if (houseOwner == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }

            return new ResponseData<OwnerMemberDTO>
            {
                Data = houseOwner,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
