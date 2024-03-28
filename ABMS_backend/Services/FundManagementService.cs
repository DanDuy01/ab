using ABMS_backend.DTO;
using ABMS_backend.DTO.FundDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Collections.Generic;
using System.Net;

namespace ABMS_backend.Services
{
    public class FundManagementService : IFundManagementRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FundManagementService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData<string> createFund(FundForInsertDTO dto)
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
                Fund fund= new Fund();
                fund.Id = Guid.NewGuid().ToString();
                fund.BuildingId = dto.buildingId;
                fund.Fund1 = dto.fund;
                fund.FundSource = dto.fundSource;
                fund.Description= dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fund.CreateUser = getUser;
                fund.CreateTime = DateTime.Now;
                fund.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Funds.Add(fund);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fund.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch(Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Created failed why " + ex.Message
                };
            }
        }

        public ResponseData<string> deleteFund(string id)
        {
            try
            {
                Fund fund = _abmsContext.Funds.Find(id);
                if (fund == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fund.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fund.ModifyUser = getUser;
                fund.ModifyTime = DateTime.Now;
                _abmsContext.Funds.Update(fund);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fund.Id,
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

        public ResponseData<List<Fund>> getAllFund(FundForSearchDTO dto)
        {
            var list = _abmsContext.Funds.Where(x =>
            (dto.id == null || x.Id == dto.id)
            && (dto.buildingId == null || x.BuildingId == dto.buildingId)
            && (dto.fund == null || x.Fund1 == dto.fund)
            && (dto.fundSource == null || x.FundSource == dto.fundSource)
            && (dto.status == null || x.Status == dto.status)).ToList();           
            return new ResponseData<List<Fund>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Fund> getFundById(string id)
        {
            Fund fund = _abmsContext.Funds.Find(id);
            if (fund == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Fund>
            {
                Data = fund,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateFund(string id, FundForInsertDTO dto)
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
                Fund fund = _abmsContext.Funds.Find(id);
                if (fund == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fund.BuildingId = dto.buildingId;
                fund.Fund1 = dto.fund;
                fund.FundSource = dto.fundSource;
                fund.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fund.ModifyUser = getUser;
                fund.ModifyTime = DateTime.Now;
                _abmsContext.Funds.Update(fund);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fund.Id,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch(Exception ex)
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
