using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Net;

namespace ABMS_backend.Services
{
    public class MemberManagerService : IMemberManagerRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;
        public MemberManagerService(abmsContext abmsContext, IMapper mapper)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
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
                resident.CreateUser = "reception";
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
                resident.CreateUser = "reception";
                resident.CreateTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.ACTIVE;
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
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Resident>> getAllMember(MemberForInsertDTO dto)
        {
            throw new NotImplementedException();
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
                resident.CreateUser = "reception";
                resident.CreateTime = DateTime.Now;
                resident.Status = (int)Constants.STATUS.ACTIVE;
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
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }
    }
}
