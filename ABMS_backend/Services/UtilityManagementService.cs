﻿using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using ABMS_backend.Utils.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Utility = ABMS_backend.Models.Utility;
using ABMS_backend.DTO.UtilityDTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ABMS_backend.Services
{
    public class UtilityManagementService : IUtilityManagementRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public UtilityManagementService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
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
                if (_abmsContext.Utilities.Any(u => u.Name == dto.name && u.BuildingId == dto.buildingId))
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        ErrMsg = "Utility with the same name already exists in this building."
                    };
                }
                Utility utility = new Utility();
                utility.Id = Guid.NewGuid().ToString();
                utility.Name = dto.name;
                utility.BuildingId = dto.buildingId;
                utility.OpenTime = dto.openTime;
                utility.CloseTime = dto.closeTime;
                utility.Location = dto.location;
                utility.NumberOfSlot = dto.numberOfSlot;
                utility.PricePerSlot = dto.pricePerSlot;
                utility.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utility.CreateUser = getUser;
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
                if (_abmsContext.Utilities.Any(u => u.Id != id && u.Name == dto.name && u.BuildingId == dto.buildingId))
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        ErrMsg = "Utility with the same name already exists in this building."
                    };
                }
                utility.Name = dto.name;
                utility.OpenTime = dto.openTime;
                utility.CloseTime = dto.closeTime;
                utility.NumberOfSlot = dto.numberOfSlot;
                utility.PricePerSlot = dto.pricePerSlot;
                utility.Description = dto.description;
                utility.Location = dto.location;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utility.ModifyUser = getUser;
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
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utility.ModifyUser = getUser;
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
                    ErrMsg = "Deleted failed why " + ex.Message
                };
            }
        }

        public ResponseData<List<Utility>> getAllUtility(UtilityForSearch dtoSearch)
        {
            var list = _abmsContext.Utilities.
                Where(x => (dtoSearch.name == null || x.Name.ToLower().Contains(dtoSearch.name.ToLower()))
                && (dtoSearch.buildingId == null || x.BuildingId == dtoSearch.buildingId)
                && (dtoSearch.status == null || x.Status == dtoSearch.status)).ToList();

            return new ResponseData<List<Utility>>
            {
                Data = list,
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

        public ResponseData<string> createUtilityDetail(UtilityDetailDTO dto)
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
            UtiliityDetail utiliityDetail = _abmsContext.UtiliityDetails.FirstOrDefault(x => x.Name.Equals(dto.name) && x.UtilityId.Equals(dto.utility_id));
            if (utiliityDetail != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = ErrorApp.UTILITY_DETAIL_EXISTED.description
                };
            }
            try
            {
                Utility utility = _abmsContext.Utilities.Find(dto.utility_id);
                if (utility == null)
                {
                    throw new CustomException(ErrorApp.UTILITY_NOT_EXISTED);
                }
                UtiliityDetail utilityDetail = new UtiliityDetail();
                utilityDetail.Id = Guid.NewGuid().ToString();
                utilityDetail.Name = dto.name;
                utilityDetail.UtilityId = dto.utility_id;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utilityDetail.CreateUser = getUser;
                utilityDetail.CreateTime = DateTime.Now;
                utilityDetail.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.UtiliityDetails.Add(utilityDetail);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilityDetail.Id,
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

        public ResponseData<List<UtiliityDetail>> getUtilityDetail(String? utilityId)
        {
            var utilityDetail = _abmsContext.UtiliityDetails.Where(x => (x.Status == (int)Constants.STATUS.ACTIVE
            && (utilityId == null || x.UtilityId ==  utilityId))).ToList();
            return new ResponseData<List<UtiliityDetail>>
            {
                Data = utilityDetail,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = utilityDetail.Count
            };
        }

        public ResponseData<string> updateUtilityDetail(string id, string name)
        {

            if (name == null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            try
            {
                UtiliityDetail utilityDetail = _abmsContext.UtiliityDetails.Find(id);
                if (utilityDetail == null) {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = ErrorApp.OBJECT_NOT_FOUND.description
                    };
                }
                UtiliityDetail detail = _abmsContext.UtiliityDetails.FirstOrDefault(x => x.Name.Equals(name));
                if (detail != null && detail.UtilityId == utilityDetail.UtilityId)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = ErrorApp.UTILITY_DETAIL_EXISTED.description
                    };
                }
                utilityDetail.Name = name;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utilityDetail.ModifyUser = getUser;
                utilityDetail.ModifyTime = DateTime.Now;
                _abmsContext.UtiliityDetails.Update(utilityDetail);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilityDetail.Id,
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

        public ResponseData<string> deleteUtilityDetail(string id)
        {
            try
            {
                UtiliityDetail utilityDetail = _abmsContext.UtiliityDetails.Find(id);
                if (utilityDetail == null)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrMsg = ErrorApp.OBJECT_NOT_FOUND.description
                    };
                }
                utilityDetail.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                utilityDetail.ModifyUser = getUser;
                utilityDetail.ModifyTime = DateTime.Now;
                _abmsContext.UtiliityDetails.Update(utilityDetail);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = utilityDetail.Id,
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

        public ResponseData<UtiliityDetail> getUtilityDetailById(string id)
        {
            UtiliityDetail utilityDetail = _abmsContext.UtiliityDetails.Find(id);
            if (utilityDetail == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<UtiliityDetail>
            {
                Data = utilityDetail,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> restore(List<string> idList)
        {
            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            foreach (string id in idList)
            {
                Utility utility = _abmsContext.Utilities.Find(id);
                if(utility == null)
                {
                    return new ResponseData<string>
                    {
                        Data = id,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrMsg = ErrorApp.OBJECT_NOT_FOUND.description
                    };
                }
                utility.Status = (int)Constants.STATUS.ACTIVE;
                utility.ModifyUser = getUser;
                utility.ModifyTime = DateTime.Now;
            }
            _abmsContext.SaveChanges();
            string jsonData = JsonConvert.SerializeObject(idList);
            return new ResponseData<string>
            {
                Data = jsonData,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = idList.Count
            };
        }

        public ResponseData<string> remove(List<string> idList)
        {
            foreach (string id in idList)
            {
                Utility utility = _abmsContext.Utilities.Find(id);
                if (utility == null)
                {
                    return new ResponseData<string>
                    {
                        Data = id,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrMsg = ErrorApp.OBJECT_NOT_FOUND.description
                    };
                }
                var detail = _abmsContext.UtiliityDetails.Where(x => x.UtilityId == id).ToList();
                foreach (var d in detail)
                {
                    var schedule = _abmsContext.UtilitySchedules.Where(x => x.UtilityDetailId == d.Id).ToList();
                    _abmsContext.UtilitySchedules.RemoveRange(schedule);
                    _abmsContext.UtiliityDetails.Remove(d);
                }
                _abmsContext.Utilities.Remove(utility);
            }
            _abmsContext.SaveChanges();
            string jsonData = JsonConvert.SerializeObject(idList);
            return new ResponseData<string>
            {
                Data = jsonData,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = idList.Count
            };
        }
        public ResponseData<bool> CheckUtilityDetailsHaveSchedules(string utilityId)
        {
            var utility = _abmsContext.Utilities
                .Where(u => u.Id == utilityId)
                .SelectMany(u => u.UtiliityDetails) // Select all UtilityDetails of the specified Utility
                .Any(ud => _abmsContext.UtilitySchedules.Any(us => us.UtilityDetailId == ud.Id && us.Status==2)); // Check if there's any schedule linked to these details

            return new ResponseData<bool>
            {
                Data = utility,
                StatusCode = utility ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                ErrMsg = utility ? "Utility Details with schedules found." : "No schedules found for any Utility Details."
            };
        }
    }
}
