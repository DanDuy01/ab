using ABMS_backend.DTO;
using ABMS_backend.DTO.HotlineDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;

namespace ABMS_backend.Services
{
    public class HotlineManagementService : IHotlineManagementRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HotlineManagementService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData<Hotline> createHotline(HotlineForInsertDTO dto)
        {
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<Hotline>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            try
            {
                Hotline hotline = new Hotline();
                hotline.Id= Guid.NewGuid().ToString();
                hotline.PhoneNumber = dto.phoneNumber;
                hotline.Name = dto.name;              
                hotline.BuildingId= dto.buildingId;
                hotline.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Hotlines.Add(hotline);
                _abmsContext.SaveChanges();
                return new ResponseData<Hotline>
                {
                    Data = hotline,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };

            }
            catch(Exception ex)
            {
                return new ResponseData<Hotline>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteHotline(string id)
        {
            try
            {
                Hotline hotline = _abmsContext.Hotlines.Find(id);
                if (hotline == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                _abmsContext.Hotlines.Remove(hotline);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = hotline.Id,
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

        public ResponseData<List<Hotline>> getAllHotline(HotlineForSearchDTO dto)
        {
            var list= _abmsContext.Hotlines.Where(x=> (dto.id == null || x.Id == dto.id)
            && (dto.buildingId == null || x.BuildingId == dto.buildingId)
            && (dto.phoneNumber == null || x.PhoneNumber == dto.phoneNumber)
            && (dto.name == null || x.Name == dto.name)).ToList();           
            return new ResponseData<List<Hotline>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Hotline> getHotlineById(string id)
        {
            Hotline hotline = _abmsContext.Hotlines.Find(id);
            if (hotline == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Hotline>
            {
                Data = hotline,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateHotline(string id, HotlineForInsertDTO dto)
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
                Hotline hotline = _abmsContext.Hotlines.Find(id);
                if (hotline == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                hotline.PhoneNumber = dto.phoneNumber;
                hotline.Name = dto.name;
                hotline.BuildingId = dto.buildingId;
                _abmsContext.Hotlines.Update(hotline);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = hotline.Id,
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
