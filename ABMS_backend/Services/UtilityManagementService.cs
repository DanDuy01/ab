﻿using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Utility = ABMS_backend.Models.Utility;

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

        

        public string GetUserFromToken(string token)
        {       
            if(token == null)
            {
                throw new CustomException(ErrorApp.FORBIDDEN);
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;

            if (jsonToken == null)
            {
                return null;
            }
            var userClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "User")?.Value;

            return userClaim;
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
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
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
                if(utility == null)
                {
                    throw new CustomException(ErrorApp.UTILITY_NOT_EXISTED);
                }
                UtiliityDetail utilityDetail = new UtiliityDetail();
                utilityDetail.Id = Guid.NewGuid().ToString();
                utilityDetail.Name = dto.name;
                utilityDetail.UtilityId = dto.utility_id;
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
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
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
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
                string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
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
                Where(x => dtoSearch.name == null || x.Name.ToLower().Contains(dtoSearch.name.ToLower())).ToList();
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

        public ResponseData<List<UtiliityDetail>> getUtilityDetail()
        {
            var utilityDetail = _abmsContext.UtiliityDetails.Where(x => x.Status == (int)Constants.STATUS.ACTIVE).ToList();
            return new ResponseData<List<UtiliityDetail>>
            {
                Data = utilityDetail,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = utilityDetail.Count
            };
        }
    }
}
