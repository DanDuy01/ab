using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using System.Net;
using System.Reflection.Metadata;

namespace ABMS_backend.Services
{
    public class BuildingService : IBuildingRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BuildingService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createBuilding(BuildingForInsertDTO dto)
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
                Building building = new Building();
                building.Id = Guid.NewGuid().ToString();
                building.Name = dto.name;
                building.Address = dto.address;
                building.NumberOfFloor = dto.number_of_floor;
                building.RoomEachFloor = dto.room_each_floor;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                building.CreateUser = getUser;
                building.CreateTime = DateTime.Now;
                building.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Buildings.Add(building);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = building.Id,
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

        public ResponseData<string> updateBuilding(string id, BuildingForInsertDTO dto)
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
                Building building = _abmsContext.Buildings.Find(id);
                if(building == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                building.Name = dto.name;
                building.Address = dto.address;
                building.NumberOfFloor = dto.number_of_floor;
                building.RoomEachFloor = dto.room_each_floor;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                building.ModifyUser = getUser;
                building.ModifyTime = DateTime.Now;
                _abmsContext.Buildings.Update(building);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = building.Id,
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

        public ResponseData<string> deleteBuilding(string id)
        {
            try
            {
                Building building = _abmsContext.Buildings.Find(id);
                if (building == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                building.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                building.ModifyUser = getUser;
                building.ModifyTime = DateTime.Now;
                _abmsContext.Buildings.Update(building);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = building.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Delete failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Building>> getBuilding(BuildingForSearchDTO dto)
        {
            var list = _abmsContext.Buildings.
                Where(x => dto.name == null || x.Name.ToLower().Contains(dto.name.ToLower())
                && (dto.address == null || x.Address.ToLower().Contains(dto.address.ToLower()))).ToList();
            return new ResponseData<List<Building>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Building> getBuildingById(string id)
        {
            Building utility = _abmsContext.Buildings.Find(id);
            if (utility == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Building>
            {
                Data = utility,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
