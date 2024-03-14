using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ABMS_backend.Services
{
    public class ParkingCardService : IParkingCardRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParkingCardService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseData<string> createParkingCard(ParkingCardForInsertDTO dto)
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

                ParkingCard parkingCard = _abmsContext.ParkingCards.FirstOrDefault(x => x.LicensePlate == dto.license_plate);
                if (parkingCard != null)
                {
                    throw new CustomException(ErrorApp.VEHICE_EXISTED);
                }
                ParkingCard card = new ParkingCard();
                card.Id = Guid.NewGuid().ToString();
                card.ResidentId = dto.resident_id;
                card.LicensePlate = dto.license_plate;
                card.Brand = dto.brand;
                card.Color = dto.color;
                card.Type = dto.type;
                card.Image = dto.image;
                card.ExpireDate = dto.expire_date;
                card.Note = dto.note;
                card.Status = (int)Constants.STATUS.SENT;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                card.CreateUser = getUser;
                card.CreateTime = DateTime.Now;
                _abmsContext.ParkingCards.Add(card);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = card.Id,
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

        public ResponseData<string> updateParkingCard(string id, ParkingCardForEditDTO dto)
        {
            // Validate DTO
            string error = dto.Validate();
            if (error != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.BadRequest, // Use BadRequest for validation errors
                    ErrMsg = error
                };
            }

            try
            {
                // Find the existing card by ID
                ParkingCard cardToUpdate = _abmsContext.ParkingCards.Find(id);
                if (cardToUpdate == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.NotFound, 
                        ErrMsg = "Parking card not found."
                    };
                }
                bool isDuplicateLicensePlate = _abmsContext.ParkingCards.Any(x => x.LicensePlate == dto.license_plate && x.Id != id);
                if (isDuplicateLicensePlate)
                {
                    throw new CustomException(ErrorApp.VEHICE_EXISTED);
                }
                cardToUpdate.ResidentId = dto.resident_id;
                cardToUpdate.Brand = dto.brand;
                cardToUpdate.Color = dto.color;
                cardToUpdate.Type = dto.type;
                cardToUpdate.Image = dto.image;
                cardToUpdate.ExpireDate = dto.expire_date;
                cardToUpdate.Note = dto.note;
                cardToUpdate.Status = dto.status;

                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                cardToUpdate.ModifyUser = getUser;
                cardToUpdate.ModifyTime = DateTime.Now;

                _abmsContext.ParkingCards.Update(cardToUpdate);
                _abmsContext.SaveChanges();

                return new ResponseData<string>
                {
                    Data = cardToUpdate.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = "Update successful."
                };
            }
            catch (CustomException ce)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    ErrMsg = ce.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Update failed due to an error: " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteParkingCard(string id)
        {
            try
            {
                ParkingCard card = _abmsContext.ParkingCards.Find(id);
                if (card == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                card.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                card.ModifyUser = getUser;
                card.ModifyTime = DateTime.Now;
                _abmsContext.ParkingCards.Update(card);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = card.Id,
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

        public ResponseData<List<ParkingCard>> getParkingCard(ParkingCardForSearchDTO dto)
        {
            var list = _abmsContext.ParkingCards.Include(x=>x.Resident).ThenInclude(r=>r.Room).
                Where(x => (dto.resident_id == null || x.ResidentId == dto.resident_id)
                && (dto.expire_date == null || x.ExpireDate == dto.expire_date)
                && (dto.status == null || x.Status == dto.status)
                && (dto.building_id == null || x.Resident.Room.BuildingId== dto.building_id)
                && (dto.room_id == null || x.Resident.RoomId == dto.room_id)).Select(x=>new ParkingCard
                {
                    Id=x.Id,
                    ResidentId=x.ResidentId,
                    LicensePlate=x.LicensePlate,
                    Brand=x.Brand,
                    Color=x.Color, 
                    Type=x.Type,
                    Image=x.Image,      
                    ExpireDate=x.ExpireDate,
                    Status=x.Status,
                    Note = x.Note,
                    CreateUser=x.CreateUser,
                    CreateTime=x.CreateTime,
                    ModifyUser=x.ModifyUser,
                    ModifyTime=x.ModifyTime,
                    Resident= new Resident
                    {
                        Id=x.Resident.Id,
                        FullName = x.Resident.FullName,
                        Room = x.Resident.Room
                    },
                    
                    
                }).ToList();
            return new ResponseData<List<ParkingCard>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<ParkingCard> getParkingCardById(string id)
        {
            ParkingCard card = _abmsContext.ParkingCards.Include(x => x.Resident).ThenInclude(r => r.Room).Where(x=>x.Id == id)
                .Select(x => new ParkingCard
                {
                    Id = x.Id,
                    ResidentId = x.ResidentId,
                    LicensePlate = x.LicensePlate,
                    Brand = x.Brand,
                    Color = x.Color,
                    Type = x.Type,
                    Image = x.Image,
                    ExpireDate = x.ExpireDate,
                    Status = x.Status,
                    Note = x.Note,
                    CreateUser = x.CreateUser,
                    CreateTime = x.CreateTime,
                    ModifyUser = x.ModifyUser,
                    ModifyTime = x.ModifyTime,
                    Resident = new Resident
                    {
                        Id = x.Resident.Id,
                        FullName = x.Resident.FullName,
                        Room = x.Resident.Room
                    },


                }).FirstOrDefault();
            if (card == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<ParkingCard>
            {
                Data = card,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
