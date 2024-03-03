﻿using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.Net;
using System.Security.Principal;

namespace ABMS_backend.Services
{
    public class UtilityManagementService : IUtilityManagementRepository
    {
        private readonly abmsContext _abmsContext;

        public UtilityManagementService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
        }

        public ResponseData<string> createUtility(UtilityForInsertDTO dto)
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
                Utility utility = new Utility();
                utility.Id = Guid.NewGuid().ToString();
                utility.Name = dto.name;
                utility.OpenTime = dto.openTime;
                utility.CloseTime = dto.closeTime;
                utility.NumberOfSlot = dto.numberOfSlot;
                utility.PricePerSlot = dto.pricePerSlot;
                utility.Description = dto.description;
                utility.CreateUser = "admin";
                utility.CreateTime = DateTime.Now;
                utility.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Utilities.Add(utility);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utility.Id,
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

        public ResponseData<string> updateUtility(string id, UtilityForInsertDTO dto)
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
                Utility utility = _abmsContext.Utilities.Find(id);
                if (utility == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                utility.Name = dto.name;
                utility.OpenTime = dto.openTime;
                utility.CloseTime = dto.closeTime;
                utility.NumberOfSlot = dto.numberOfSlot;
                utility.PricePerSlot = dto.pricePerSlot;
                utility.Description = dto.description;
                utility.ModifyUser = "admin";
                utility.ModifyTime = DateTime.Now;
                _abmsContext.Utilities.Update(utility);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utility.Id,
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

        public ResponseData<string> deleteUtility(string id)
        {
            try
            {
                //xoa tien ich
                Utility utility = _abmsContext.Utilities.Find(id);
                if (utility == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                utility.Status = (int)Constants.STATUS.IN_ACTIVE;
                utility.ModifyUser = "admin";
                utility.ModifyTime = DateTime.Now;
                _abmsContext.Utilities.Update(utility);
                _abmsContext.SaveChanges();
                
                //xoa lich cua tien ich
                var utility_schedule = _abmsContext.UtilitySchedules.Where(x => x.UtilityId == id && 
                (x.Status == 2 || x.Status == 3)).ToList();
                foreach(UtilitySchedule us in utility_schedule)
                {
                    us.Status = (int)Constants.STATUS.REJECTED;
                    _abmsContext.UtilitySchedules.Update(us);
                    _abmsContext.SaveChanges();
                }
                return new ResponseData<string>
                {
                    Data = utility.Id,
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

        public ResponseData<List<Utility>> getAllUtility(UtilityForSearch dtoSearch)
        {
            var list = _abmsContext.Utilities.
                Where(x => dtoSearch.name == null || x.Name.ToLower().Contains(dtoSearch.name.ToLower())).ToList();
            return new ResponseData<List<Utility>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<List<UtilityForInsertDTO>> getUtility(UtilityForSearch dtoSearch)
        {
            var list = _abmsContext.Utilities.
                Where((x => dtoSearch.name == null || x.Name.ToLower().Contains(dtoSearch.name.ToLower())
                && x.Status == (int)Constants.STATUS.ACTIVE)).ToList();
            List<UtilityForInsertDTO> listDto = new List<UtilityForInsertDTO>();
            foreach (var item in list)
            {
                UtilityForInsertDTO dto = new UtilityForInsertDTO();
                dto.name = item.Name;
                dto.openTime = item.OpenTime;
                dto.closeTime = item.CloseTime;
                dto.numberOfSlot = item.NumberOfSlot;
                dto.pricePerSlot = item.PricePerSlot;
                dto.description = item.Description;
                listDto.Add(dto);
            }
            return new ResponseData<List<UtilityForInsertDTO>>
            {
                Data = listDto,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Utility> getUtilityById(string id)
        {
            Utility utility = _abmsContext.Utilities.Find(id);
            if (utility == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Utility>
            {
                Data = utility,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        
    }
}
