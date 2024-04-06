using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using System.Net;
using ABMS_backend.Utils.Exceptions;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ABMS_backend.Utils.Token;
using Microsoft.EntityFrameworkCore;
using ABMS_backend.DTO.ElevatorDTO;

namespace ABMS_backend.Services
{
    public class ElevatorService : IElevatorRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ElevatorService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createElevator(ElevatorForInsertDTO dto)
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
               
                Elevator elevator = new Elevator();
                elevator.Id = Guid.NewGuid().ToString();
                elevator.RoomId = dto.room_id;
                elevator.StartTime = dto.start_time;
                elevator.EndTime = dto.end_time;
                elevator.Description = dto.description;
                elevator.Status = (int)Constants.STATUS.SENT;
                _abmsContext.Elevators.Add(elevator);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = elevator.Id,
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

        public ResponseData<string> updateElevator(string id, ElevatorForEditDTO dto)
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
              
                Elevator elevator = _abmsContext.Elevators.Find(id);
                if (elevator == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                elevator.StartTime = dto.start_time;
                elevator.EndTime = dto.end_time;
                elevator.Description = dto.description;
                _abmsContext.Elevators.Update(elevator);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = elevator.Id,
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

        public ResponseData<string> manageElevator(string id, ElevatorForManageDTO dto)
        {
            Elevator elevator = _abmsContext.Elevators.Find(id);
            if (elevator == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            bool hasConflict = _abmsContext.Elevators.Any(otherElevator =>
         otherElevator.Id != id &&
         otherElevator.Status == 3 && dto.status !=4 &&
         (elevator.StartTime < otherElevator.EndTime && elevator.EndTime > otherElevator.StartTime));

            if (hasConflict)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    ErrMsg = "Time slot conflict"
                };
            }
            elevator.Status = dto.status;
            elevator.Response = dto.response;
            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            elevator.ApproveUser = getUser;
            _abmsContext.Elevators.Update(elevator);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = elevator.Id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> deleteElevator(string id)
        {
            try
            {
                Elevator elevator = _abmsContext.Elevators.Find(id);
                if (elevator == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                //elevator.Status = (int)Constants.STATUS.IN_ACTIVE;
                _abmsContext.Elevators.Remove(elevator);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = elevator.Id,
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

        public ResponseData<List<Elevator>> getElevator(ElevatorForSearchDTO dto)
        {
            var list = _abmsContext.Elevators.Include(x=>x.Room).
                Where(x => (dto.room_id == null || x.RoomId == dto.room_id)
                && (dto.time == null || (x.StartTime <= dto.time && dto.time <= x.EndTime))
                && (dto.status == null || x.Status == dto.status) && 
                (dto.building_id== null || x.Room.BuildingId == dto.building_id)).Select(x=> new Elevator
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Description = x.Description,
                    Status = x.Status,
                    RoomId = x.RoomId,
                    Room = x.Room,
                    ApproveUser = x.ApproveUser
                }).ToList();
            return new ResponseData<List<Elevator>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Elevator> getElevatorById(string id)
        {
            Elevator elevator = _abmsContext.Elevators.Find(id);
            if (elevator == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Elevator>
            {
                Data = elevator,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
