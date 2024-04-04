using ABMS_backend.DTO;
using ABMS_backend.DTO.FeeDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Net;

namespace ABMS_backend.Services
{
    public class FeeManagementService : IFeeManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeeManagementService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData<string> createFee(FeeForInsertDTO dto)
        {
            // Validate DTO as before
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = error
                };
            }
            if (_abmsContext.Fees.Any(f => f.ServiceName == dto.feeName))
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.Conflict, 
                    ErrMsg = "Service name already exists."
                };
            }

            // Create fee object and populate data
            Fee fee = new Fee();
            fee.Id = Guid.NewGuid().ToString();
            fee.BuildingId = dto.buildingId;
            fee.ServiceName = dto.feeName;
            fee.Price = dto.price;
            fee.Unit = dto.unit;
            fee.EffectiveDate = dto.effectiveDate;
            fee.ExpireDate = dto.expireDate;
            fee.Description = dto.description;
            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            fee.CreateUser = getUser;
            fee.CreateTime = DateTime.Now;
            fee.Status = (int)Constants.STATUS.ACTIVE;

            try
            {
                _abmsContext.Fees.Add(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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
        public ResponseData<string> AssignFeesToAllRoomsInBuilding(string buildingId)
        {
            var excludedFeeNames = new List<string> { "Ô tô", "Xe đạp", "Xe máy","Xe đạp điện" };
            var fees = _abmsContext.Fees.Where(f => f.BuildingId == buildingId && !excludedFeeNames.Contains(f.ServiceName)).ToList();
            var rooms = _abmsContext.Rooms.Where(r => r.BuildingId == buildingId).ToList();

            foreach (var room in rooms)
            {
                if (room.RoomArea > 0)
                {
                    foreach (var fee in fees)
                    {
                        if (!_abmsContext.RoomServices.Any(rs => rs.RoomId == room.Id && rs.FeeId == fee.Id))
                        {
                            RoomService roomService = new RoomService
                            {
                                Id = Guid.NewGuid().ToString(),
                                RoomId = room.Id,
                                FeeId = fee.Id,
                                Amount = (int)room.RoomArea,
                                Description = "Automatically assigned",
                                CreateUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]),
                                CreateTime = DateTime.Now,
                                Status = (int)Constants.STATUS.ACTIVE
                            };
                            _abmsContext.RoomServices.Add(roomService);
                        }
                    }
                }
            }

            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = "Assigned fees to all rooms successfully",
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> deleteFee(string id)
        {
            try
            {
                Fee fee = _abmsContext.Fees.Find(id);
                if (fee == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fee.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fee.ModifyUser = getUser;
                fee.ModifyTime = DateTime.Now;
                _abmsContext.Fees.Update(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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

        public ResponseData<List<Fee>> getAllFee(FeeForSearchDTO dto)
        {
            var list = _abmsContext.Fees.Where(x =>
            (dto.feeName == null || x.ServiceName.ToLower()
                 .Contains(dto.feeName.ToLower()))
                 && (dto.buildingId == null || x.BuildingId == dto.buildingId)
                 && (dto.price == null || x.Price == dto.price)
                 && (dto.unit == null || x.Unit == dto.unit)
                 && (dto.effective_Date == null || x.EffectiveDate == dto.effective_Date)).ToList();

            return new ResponseData<List<Fee>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Fee> getFeeById(string id)
        {
            Fee fee = _abmsContext.Fees.Find(id);
            if (fee == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Fee>
            {
                Data = fee,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
        public ResponseData<bool> CheckSpecificFeesExistence()
        {
            var feeNames = new List<string> { "Ô tô", "Xe đạp", "Xe máy","Xe đạp điện" };
            bool exists = _abmsContext.Fees.Any(f => feeNames.Contains(f.ServiceName));

            return new ResponseData<bool>
            {
                Data = exists,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<List<string>> CheckRoomsMissingFees(string buildingId)
        {
            var excludedFeeNames = new List<string> { "Ô tô", "Xe đạp", "Xe máy", "Xe đạp điện" };
            var fees = _abmsContext.Fees
                .Where(f => f.BuildingId == buildingId && !excludedFeeNames.Contains(f.ServiceName))
                .Select(f => f.Id)
                .ToList();

            var roomsWithAllFees = _abmsContext.RoomServices
                .Where(rs => fees.Contains(rs.FeeId) && rs.Room.BuildingId == buildingId)
                .Select(rs => rs.RoomId)
                .Distinct()
                .ToList();

            var roomsMissingFees = _abmsContext.Rooms
                .Where(r => r.BuildingId == buildingId && !roomsWithAllFees.Contains(r.Id))
                .Select(r => r.RoomNumber)
                .ToList();

            return new ResponseData<List<string>>
            {
                Data = roomsMissingFees,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
        public ResponseData<string> updateFee(string id, FeeForInsertDTO dto)
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
                Fee fee = _abmsContext.Fees.Find(id);
                if (fee == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fee.ServiceName = dto.feeName;
                fee.Price = dto.price;
                fee.Unit = dto.unit;
                fee.EffectiveDate = dto.effectiveDate;
                fee.ExpireDate = dto.expireDate;
                fee.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fee.ModifyUser = getUser;
                fee.ModifyTime = DateTime.Now;
                _abmsContext.Fees.Update(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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
