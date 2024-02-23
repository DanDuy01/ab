using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using System.Net;
using AutoMapper;
using ABMS_backend.Utils.Exceptions;
using System.Collections.Generic;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class ResidentAccountManagementService : IResidentAccountManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;
        public ResidentAccountManagementService(abmsContext abmsContext, IMapper mapper)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
        }

        ResponseData<string> IResidentAccountManagementRepository.createResidentAccount(ResidentForInsertDTO dto)
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
                Resident r = new Resident();
                r.Id = Guid.NewGuid().ToString();
                r.RoomId = dto.roomId;
                r.FullName = dto.fullName;
                r.DateOfBirth = dto.dob;
                r.Gender = dto.gender;
                r.Phone = dto.phone;
                r.CreateUser = "admin";
                r.CreateTime = DateTime.Now;
                r.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Residents.Add(r);
                _abmsContext.SaveChanges();

                return new ResponseData<string>
                {
                    Data = r.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create account failed: " + ex.Message
                };
            }
        }

        ResponseData<string> IResidentAccountManagementRepository.updateResidentAccount(string id, ResidentForInsertDTO dto)
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
                Resident r = _abmsContext.Residents.Find(id);
                if (r == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                r.RoomId = dto.roomId;
                r.FullName = dto.fullName;
                r.DateOfBirth = dto.dob;
                r.Gender = dto.gender;
                r.Phone = dto.phone;
                r.CreateUser = "admin";
                r.CreateTime = DateTime.Now;
                r.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Residents.Update(r);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = r.Id,
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

        ResponseData<string> IResidentAccountManagementRepository.deleteResidentAccount(string id)
        {
            try
            {
                Resident r = _abmsContext.Residents.Find(id);
                if (r == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                r.Status = (int)Constants.STATUS.IN_ACTIVE;
                r.ModifyUser = "admin";
                r.ModifyTime = DateTime.Now;
                _abmsContext.Residents.Update(r);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = r.Id,
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

        ResponseData<List<Resident>> IResidentAccountManagementRepository.getResidentAccount(ResidentForSearchDTO dto)
        {
            var list = _abmsContext.Residents
                .Where(x => (dto.searchMsg == null || x.Phone.Contains(dto.searchMsg.ToLower())
                || x.FullName.ToLower().Contains(dto.searchMsg.ToLower()))
                && (dto.roomId == null || x.RoomId.Equals(dto.roomId.ToLower()))
                && (dto.status == null || x.Status == dto.status)).ToList();
            return new ResponseData<List<Resident>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        ResponseData<Resident> IResidentAccountManagementRepository.getResidentById(string id)
        {
            Resident r = _abmsContext.Residents.Find(id);
            if (r == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Resident>
            {
                Data = r,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
