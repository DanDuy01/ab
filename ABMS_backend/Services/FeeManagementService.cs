using ABMS_backend.DTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using System.Net;

namespace ABMS_backend.Services
{
    public class FeeManagementService : IFeeManagementRepository
    {
        private readonly abmsContext _abmsContext;
        private IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeeManagementService(abmsContext abmsContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseData<string> createFee(FeeForInsertDTO dto)
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
                Fee fee = new Fee();
                fee.Id = Guid.NewGuid().ToString();
                fee.ServiceName = dto.feeName;
                fee.Price = dto.price;
                fee.Unit = dto.unit;
                fee.EffectiveDate = dto.effectiveDate;
                fee.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fee.CreateUser = getUser;
                fee.CreateTime = DateTime.Now;
                fee.Status = (int)Constants.STATUS.ACTIVE;
                _abmsContext.Fees.Add(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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

        public ResponseData<string> deleteFee(string id)
        {
            try
            {
                Fee fee = _abmsContext.Fees.Find(id);
                if (fee == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fee.Status = (int)Constants.STATUS.IN_ACTIVE;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fee.ModifyUser = getUser;
                fee.ModifyTime = DateTime.Now;
                _abmsContext.Fees.Update(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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

        public ResponseData<List<Fee>> getAllFee(FeeForSearchDTO dto)
        {
            var list = _abmsContext.Fees.Where(x =>
            (dto.feeName == null || x.ServiceName.ToLower()
                 .Contains(dto.feeName.ToLower()))
            && (dto.price == null || x.Price == dto.price)
            && (dto.unit == null || x.Unit == dto.unit)
            && (dto.effective_Date == null || x.EffectiveDate == dto.effective_Date)).ToList();

            return new ResponseData<List<Fee>>
            {
                Data = list,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description,
                Count = list.Count
            };
        }

        public ResponseData<Fee> getFeeById(string id)
        {
            Fee fee = _abmsContext.Fees.Find(id);
            if (fee == null)
            {
                throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
            }
            return new ResponseData<Fee>
            {
                Data = fee,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> updateFee(string id, FeeForInsertDTO dto)
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
                Fee fee = _abmsContext.Fees.Find(id);
                if (fee == null)
                {
                    throw new CustomException(ErrorApp.OBJECT_NOT_FOUND);
                }
                fee.ServiceName = dto.feeName;
                fee.Price = dto.price;
                fee.Unit = dto.unit;
                fee.EffectiveDate = dto.effectiveDate;
                fee.Description = dto.description;
                string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                fee.ModifyUser = getUser;
                fee.ModifyTime = DateTime.Now;
                _abmsContext.Fees.Update(fee);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = fee.Id,
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
