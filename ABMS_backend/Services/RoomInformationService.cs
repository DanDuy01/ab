using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Net;

namespace ABMS_backend.Services
{
    public class RoomInformationService : IRoomInformationRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        public RoomInformationService(abmsContext abmsContext, IMapper mapper)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
        }
        public ResponseData<string> createRoomInformation(RoomForInsertDTO dto)
        {
            try
            {
                Room room = new Room();
                room.Id = Guid.NewGuid().ToString();
                room.BuildingId = dto.buildingId;
                room.RoomNumber = dto.roomNumber;
                room.RoomArea = dto.roomArea;
                room.NumberOfResident = dto.numberOfResident;               
                room.CreateUser = "resident";
                room.CreateTime = DateTime.Now;
                room.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Rooms.Add(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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

        public ResponseData<string> deleteRoomInformation(string id)
        {
            try
            {
                Room room = _abmsContext.Rooms.Find(id);

                if (room == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }              
                room.CreateUser = "resident";
                room.CreateTime = DateTime.Now;
                room.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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

        public ResponseData<List<Room>> getRoomInformation(RoomForSearchDTO dto)
        {
            throw new NotImplementedException();
        }

        public ResponseData<Room> getRoomInformationById(string id)
        {
            Room room = _abmsContext.Rooms.Find(id);
            if (room == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Room>
            {
                Data = room,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateRoomInformation(string id, RoomForInsertDTO dto)
        {
            try
            {
                Room room = _abmsContext.Rooms.Find(id);
               
                if (room == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                room.BuildingId = dto.buildingId;
                room.RoomNumber = dto.roomNumber;
                room.RoomArea = dto.roomArea;
                room.NumberOfResident = dto.numberOfResident;
                room.CreateUser = "resident";
                room.CreateTime = DateTime.Now;
                room.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Rooms.Update(room);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = room.Id,
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
