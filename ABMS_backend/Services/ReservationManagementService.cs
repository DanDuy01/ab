using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class ReservationManagementService : IReservationManagementRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationManagementService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createReservation(ReservationForInsertDTO dto)
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
            UtilitySchedule schedule = _abmsContext.UtilitySchedules.FirstOrDefault(
                x => x.BookingDate == dto.booking_date && x.Slot == dto.slot
                && x.UtilityDetailId == dto.utility_detail_id);
            if (schedule != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = ErrorApp.BOOKING_EXISTED.description
                };
            }
            try
            {
                UtilitySchedule utilitySchedule = new UtilitySchedule();
                utilitySchedule.Id = Guid.NewGuid().ToString();
                utilitySchedule.RoomId = dto.room_id;
                utilitySchedule.UtilityDetailId = dto.utility_detail_id;
                utilitySchedule.Slot = dto.slot;
                utilitySchedule.BookingDate = dto.booking_date;
                utilitySchedule.NumberOfPerson = dto.number_of_person;
                utilitySchedule.TotalPrice = dto.total_price;
                utilitySchedule.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utilitySchedule.ApproveUser = getUser;
                utilitySchedule.Status = (int)Constants.STATUS.SENT;
                _abmsContext.UtilitySchedules.Add(utilitySchedule);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilitySchedule.Id,
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

        public ResponseData<string> updateReservation(String id, ReservationForInsertDTO dto)
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
                UtilitySchedule utilitySchedule = _abmsContext.UtilitySchedules.Find(id);
                if (utilitySchedule == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = ErrorApp.OBJECT_NOT_FOUND.description
                    };
                }
                utilitySchedule.Slot = dto.slot;
                utilitySchedule.UtilityDetailId = dto.utility_detail_id;
                utilitySchedule.Slot = dto.slot;
                utilitySchedule.BookingDate = dto.booking_date;
                utilitySchedule.NumberOfPerson = dto.number_of_person;
                utilitySchedule.TotalPrice = dto.total_price;
                utilitySchedule.Description = dto.description;
                _abmsContext.UtilitySchedules.Update(utilitySchedule);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilitySchedule.Id,
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

        public ResponseData<string> deleteReservation(string id)
        {
            try
            {
                UtilitySchedule utilitySchedule = _abmsContext.UtilitySchedules.Find(id);
                if (utilitySchedule == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                _abmsContext.UtilitySchedules.Remove(utilitySchedule);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilitySchedule.Id,
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

        public ResponseData<List<ReservationResponseDTO>> getReservation(ResevationForResidentSearchDTO dto)
        {
            var utilitySchedules = _abmsContext.UtilitySchedules.Include(x => x.UtilityDetail.Utility).
                Where(x => (dto.roomId == null || x.RoomId == dto.roomId) &&
                (dto.utilityId == null || x.UtilityDetail.UtilityId == dto.utilityId) &&
                (dto.utilityDetailId == null || x.UtilityDetailId == dto.utilityDetailId) &&
                (dto.bookingDate == null || x.BookingDate == dto.bookingDate)).ToList();
            List < ReservationResponseDTO > dtoList = new List<ReservationResponseDTO> ();
            foreach (var schedule in utilitySchedules)
            {
                ReservationResponseDTO response = new   ();
                response.id = schedule.Id;
                response.room_id = schedule.RoomId;
                response.utility = schedule.UtilityDetail.Utility.Name;
                response.utility_detail_id = schedule.UtilityDetailId;
                response.utility_detail_name = schedule.UtilityDetail.Name;
                response.slot = schedule.Slot;
                response.booking_date = schedule.BookingDate;
                response.number_of_person = schedule.NumberOfPerson;
                response.total_price = schedule.TotalPrice;
                response.description = schedule.Description;
                response.approve_user = schedule.ApproveUser;
                response.status = schedule.Status;
                dtoList.Add(response);
            }
            return new ResponseData<List<ReservationResponseDTO>>
            {
                Data = dtoList,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = utilitySchedules.Count
            };
        }

        public ResponseData<string> manageReservation(string id, int status)
        {
            UtilitySchedule utilitySchedule = _abmsContext.UtilitySchedules.Find(id);
            if (utilitySchedule == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            utilitySchedule.Status = status;
            if (_httpContextAccessor.HttpContext.Session.GetString("user") == null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ErrMsg = ErrorApp.FORBIDDEN.description
                };
            }
            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            utilitySchedule.ApproveUser = getUser;
            _abmsContext.UtilitySchedules.Update(utilitySchedule);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<ReservationResponseDTO> getReservationById(string id)
        {
            var utilitySchedule = _abmsContext.UtilitySchedules
                        .Where(us => us.Id == id)
                        .Include(us => us.UtilityDetail)
                            .ThenInclude(ud => ud.Utility)
                        .FirstOrDefault();
            if (utilitySchedule == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            ReservationResponseDTO dto = new ReservationResponseDTO();
            dto.id = utilitySchedule.Id;
            dto.utility_detail_id = utilitySchedule.UtilityDetail.UtilityId;
            dto.room_id = utilitySchedule.RoomId;
            dto.utility = utilitySchedule.UtilityDetail.Utility.Name;
            dto.utility_detail_name = utilitySchedule.UtilityDetail.Name;
            dto.slot = utilitySchedule.Slot;
            dto.booking_date = utilitySchedule.BookingDate;
            dto.number_of_person = utilitySchedule.NumberOfPerson;
            dto.total_price = utilitySchedule.TotalPrice;
            dto.description = utilitySchedule.Description;
            dto.approve_user = utilitySchedule.ApproveUser;
            dto.status = utilitySchedule.Status;
            return new ResponseData<ReservationResponseDTO>
            {
                Data = dto,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
