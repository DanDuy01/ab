using ABMS_backend.DTO;
using ABMS_backend.DTO.RoomServiceDTO;
using ABMS_backend.DTO.ServiceChargeDTO;
using ABMS_backend.DTO.UtilityDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace ABMS_backend.Services
{
    public class ServiceChargeService : IServiceChargeRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceChargeService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createServiceCharge(ServiceChargeForInsertDTO dto)
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
                List<string> list = new List<string>();
                var rooms = _abmsContext.Rooms.Include(x => x.RoomServices).Where(x => x.BuildingId == dto.building_id).ToList();
                foreach (var room in rooms)
                {
                    ServiceCharge service = _abmsContext.ServiceCharges.FirstOrDefault
                    (x => x.RoomId == room.Id && x.Month == dto.month && x.Year == dto.year);
                    if (service != null)
                    {
                        throw new CustomException(ErrorApp.SERVICE_CHARGE_EXISTED);
                    }
                    ServiceCharge serviceCharge = new ServiceCharge();
                    serviceCharge.Id = Guid.NewGuid().ToString();
                    serviceCharge.RoomId = room.Id;
                    serviceCharge.Month = dto.month;
                    serviceCharge.Year = dto.year;
                    string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                    serviceCharge.CreateUser = getUser;
                    serviceCharge.CreateTime = DateTime.Now;
                    serviceCharge.Status = (int)Constants.STATUS.NOT_PAID;

                    var roomService = _abmsContext.RoomServices.Include(x => x.Fee).
                        Where(x => x.RoomId == room.Id).ToList();
                    foreach (var item in roomService)
                    {
                        serviceCharge.TotalPrice += item.Amount * item.Fee.Price;
                    }
                    list.Add(serviceCharge.Id);
                    _abmsContext.ServiceCharges.Add(serviceCharge);
                }               
                _abmsContext.SaveChanges();
                string jsonData = JsonConvert.SerializeObject(list);
                return new ResponseData<string>
                {
                    Data = jsonData,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description,
                    Count = list.Count
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

        public ResponseData<string> updateServiceCharge(string id, string? description, int? status)
        {
            try
            {
                ServiceCharge serviceCharge = _abmsContext.ServiceCharges.Find(id);
                if (serviceCharge == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                ServiceCharge service = _abmsContext.ServiceCharges.FirstOrDefault
                    (x => x.RoomId == serviceCharge.RoomId && x.Month == serviceCharge.Month && x.Year == serviceCharge.Year);
                if (service != null && service.Id != id)
                {
                    throw new CustomException(ErrorApp.SERVICE_CHARGE_EXISTED);
                }
                serviceCharge.Description = description;
                if(status != null)
                {
                    serviceCharge.Status = (int)status;
                }              
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                serviceCharge.ModifyUser = getUser;
                serviceCharge.ModifyTime = DateTime.Now;
                _abmsContext.ServiceCharges.Update(serviceCharge);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = serviceCharge.Id,
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

        public ResponseData<string> deleteServiceCharge(string id)
        {
            try
            {
                ServiceCharge serviceCharge = _abmsContext.ServiceCharges.Find(id);
                if (serviceCharge == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }             
                _abmsContext.ServiceCharges.Remove(serviceCharge);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = serviceCharge.Id,
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

        public ResponseData<List<ServiceCharge>> getServiceCharge(ServiceChargeForSearchDTO dto)
        {
            var list = _abmsContext.ServiceCharges.Where(x =>
            (dto.room_id == null || x.RoomId == dto.room_id)
                && (dto.month == null || x.Month == dto.month)
                && (dto.year == null || x.Year == dto.year)).ToList();
            return new ResponseData<List<ServiceCharge>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<ServiceCharge> getServiceChargeById(string id)
        {
            ServiceCharge serviceCharge = _abmsContext.ServiceCharges.Find(id);
            if (serviceCharge == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<ServiceCharge>
            {
                Data = serviceCharge,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<List<ServiceChargeResponseDTO>> getTotal(string room_id, int? status)
        {
            var serviceCharge = _abmsContext.ServiceCharges
                .Where(x => x.RoomId == room_id && (status == null || x.Status == status)).ToList();
            List< ServiceChargeResponseDTO > list = new List< ServiceChargeResponseDTO >();
            foreach (var item in serviceCharge)
            {
                ServiceChargeResponseDTO dto = new ServiceChargeResponseDTO();
                dto.year = item.Year;
                dto.month = item.Month;
                dto.total = item.TotalPrice;
                dto.status = item.Status;
                var roomService = _abmsContext.RoomServices.Include(x => x.Fee).Where(x => x.RoomId ==  item.RoomId);
                List<RoomServiceResponseDTO> listDetail = new List<RoomServiceResponseDTO>();
                foreach (RoomService rs in roomService)
                {
                    RoomServiceResponseDTO detail = new RoomServiceResponseDTO();
                    detail.service_name = rs.Fee.ServiceName;
                    detail.fee = rs.Fee.Price;
                    detail.amount = rs.Amount;
                    detail.total = rs.Fee.Price * rs.Amount;
                    listDetail.Add(detail);
                }
                dto.detail = listDetail;
                list.Add(dto);
            }           
            return new ResponseData<List<ServiceChargeResponseDTO>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        
    }
}
