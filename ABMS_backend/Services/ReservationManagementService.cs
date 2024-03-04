using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.Collections.Generic;
using System.Net;

namespace ABMS_backend.Services
{
    public class ReservationManagementService : IReservationManagementRepository
    {
        private readonly abmsContext _abmsContext;

        public ReservationManagementService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
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
            UtilitySchedule schedule = _abmsContext.UtilitySchedules.FirstOrDefault(x => x.BookingDate == dto.BookingDate && x.Slot == dto.Slot);
/*            if(schedule != null)
            {
                throw new CustomException(ErrorApp.BOOKING_EXISTED);
            }*/
            try
            {
                UtilitySchedule utilitySchedule = new UtilitySchedule();
                utilitySchedule.Id = Guid.NewGuid().ToString();
                utilitySchedule.RoomId = dto.RoomId;
                utilitySchedule.UtilityId = dto.UtilityId;
                utilitySchedule.Slot = dto.Slot;
                utilitySchedule.BookingDate = dto.BookingDate;
                utilitySchedule.NumberOfPerson = dto.NumberOfPerson;
                utilitySchedule.TotalPrice = dto.TotalPrice;
                utilitySchedule.Description = dto.Description;
                utilitySchedule.ApproveUser = "admin";
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
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                utilitySchedule.Slot = dto.Slot;
                utilitySchedule.BookingDate = dto.BookingDate;
                utilitySchedule.NumberOfPerson = dto.NumberOfPerson;
                utilitySchedule.TotalPrice = dto.TotalPrice;
                utilitySchedule.Description = dto.Description;
                _abmsContext.UtilitySchedules.Update(utilitySchedule);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilitySchedule.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }

        public ResponseData<List<ReservationResponseDTO>> getReservation(ResevationForResidentSearchDTO dto)
        {
            var utilitySchedules = _abmsContext.UtilitySchedules.
                Where(x => (dto.roomId == null || x.RoomId == dto.roomId) &&
                (dto.utilityId == null || x.UtilityId == dto.utilityId) &&
                (dto.bookingDate == null || x.BookingDate == dto.bookingDate)).ToList();
            List < ReservationResponseDTO > dtoList = new List<ReservationResponseDTO> ();
            foreach (var schedule in utilitySchedules)
            {
                ReservationResponseDTO response = new ReservationResponseDTO();
                response.room_id = schedule.RoomId;
                response.utility_id = schedule.UtilityId;
                response.slot = schedule.Slot;
                response.booking_date = schedule.BookingDate;
                response.number_of_person = schedule.NumberOfPerson;
                response.total_price = schedule.TotalPrice;
                response.description = schedule.Description;
                response.approve_user = schedule.ApproveUser;
                response.status = schedule.Status;
                dtoList.Add (response);
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
            utilitySchedule.ApproveUser = "admin";
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
            UtilitySchedule utilitySchedule = _abmsContext.UtilitySchedules.Find(id);
            if (utilitySchedule == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            ReservationResponseDTO dto = new ReservationResponseDTO();
            dto.room_id = utilitySchedule.RoomId;
            dto.utility_id = utilitySchedule.UtilityId;
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
