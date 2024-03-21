using ABMS_backend.DTO;
using ABMS_backend.DTO.ServiceTypeDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ABMS_backend.Services
{
    public class Service_TypeService : IServiceTypeRepository
    {
        private readonly abmsContext _abmsContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Service_TypeService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<ServiceType> createServiceType(ServiceTypeInsert dto)
        {
            //validate
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<ServiceType>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }

            try
            {
                ServiceType st = new ServiceType();
                st.Id = Guid.NewGuid().ToString();
                st.BuildingId = dto.buildingId;
                st.Name = dto.name;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                st.CreateUser = getUser;
                st.CreateTime = DateTime.Now;
                st.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.ServiceTypes.Add(st);
                _abmsContext.SaveChanges();
                return new ResponseData<ServiceType>
                {
                    Data = st,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<ServiceType>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Create failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> updateServiceType(string id, ServiceTypeInsert dto)
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
                ServiceType st = _abmsContext.ServiceTypes.Find(id);
                if (st == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }

                st.BuildingId = dto.buildingId;
                st.Name = dto.name;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                st.ModifyUser = getUser;
                st.ModifyTime = DateTime.Now;
                _abmsContext.ServiceTypes.Update(st);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = st.Id,
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

        public ResponseData<string> deleteServiceType(string id)
        {
            try
            {
                ServiceType st = _abmsContext.ServiceTypes.Find(id);
                if (st == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }

                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                st.ModifyUser = getUser;
                st.ModifyTime = DateTime.Now;
                st.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.ServiceTypes.Update(st);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = st.Id,
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

        public ResponseData<ServiceType> getServiceTypeById(string id)
        {
            ServiceType st = _abmsContext.ServiceTypes.Find(id);
            if (st == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<ServiceType>
            {
                Data = st,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<List<ServiceType>> getAllServiceType(ServiceTypeSearch dto)
        {
            var list = _abmsContext.ServiceTypes.Include(x => x.Building)
                .Where(x => (dto.buildingId == null || x.BuildingId == dto.buildingId)
                    && (dto.name == null || x.Name.ToLower().Contains(dto.name.ToLower()))
                    && (dto.status == null || x.Status == dto.status))
                .Select(x => new ServiceType
                {
                    Id = x.Id,
                    BuildingId = x.BuildingId,
                    Name = x.Name,
                    CreateUser = x.CreateUser,
                    CreateTime = x.CreateTime,
                    ModifyUser = x.ModifyUser,
                    ModifyTime = x.ModifyTime,
                    Status = x.Status
                })
                .ToList();
            return new ResponseData<List<ServiceType>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }
    }
}
