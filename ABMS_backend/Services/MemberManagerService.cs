using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using AutoMapper;
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

        public string GetUserFromToken(string token)
        {
            if (token == null)
            {
                throw new CustomException(ErrorApp.FORBIDDEN);
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;

            if (jsonToken == null)
            {
                return null;
            }
            var userClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "User")?.Value;

            return userClaim;
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
                Resident resident = new Resident();
                resident.Id = Guid.NewGuid().ToString();
                resident.RoomId = dto.roomId;
                resident.FullName = dto.fullName;
                resident.DateOfBirth = dto.dob;
                resident.Gender = dto.gender;
                resident.IsHouseholder = dto.isHouseHolder;
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                resident.CreateUser = getUser;
                resident.CreateTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Residents.Add(resident);
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
                Resident resident = new Resident();
                Resident r = _abmsContext.Residents.Find(id);
                if (r == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                resident.ModifyUser = getUser;
                resident.ModifyTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.IN_ACTIVE;
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
                    ErrMsg = "Deleted failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Resident>> getAllMember(MemberForSearchDTO dto)
        {
            var list = _abmsContext.Residents.
                 Where(x => dto.fullName == null || x.FullName.ToLower().Contains(dto.fullName.ToLower())).ToList();
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
                Resident resident = new Resident();
                Resident r = _abmsContext.Residents.Find(id);
                if (r == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                resident.RoomId = dto.roomId;
                resident.FullName = dto.fullName;
                resident.DateOfBirth = dto.dob;
                resident.Gender = dto.gender;
                resident.IsHouseholder = dto.isHouseHolder;
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
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
    }
}
