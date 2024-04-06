using ABMS_backend.DTO;
using ABMS_backend.DTO.FundDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Token;
using ABMS_backend.Utils.Validates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Net;

namespace ABMS_backend.Services
{
    public class FundManagementService : IFundManagementRepository
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FundManagementService(abmsContext abmsContext, IHttpContextAccessor httpContextAccessor)
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
                //fund.Status = (int)Constants.STATUS.IN_ACTIVE;
                //string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                //fund.ModifyUser = getUser;
                //fund.ModifyTime = DateTime.Now;
                //_abmsContext.Funds.Update(fund);
                _abmsContext.Remove(fund);
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


        public byte[] ExportData(string buildingId)
        {
            try
            {
                var funds = _abmsContext.Funds.Include(x => x.Building).Where(x => x.Status == 1 && x.BuildingId == buildingId).ToList();
                Building building = _abmsContext.Buildings.Find(buildingId);
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Funds");

                    // Add title
                    var titleCell = worksheet.Cells["A1:D1"];
                    titleCell.Merge = true; // Merge cells from A1 to F1
                    titleCell.Value = building.Name + "'s Fund Statistics";
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.Font.Size = 14;

                    // Add headers
                    worksheet.Cells["A1"].Value = "Fund";
                    worksheet.Cells["B1"].Value = "Fund Source";
                    worksheet.Cells["C1"].Value = "Description";

                    // Add data
                    int row = 4;
                    foreach (var fund in funds)
                    {
                        worksheet.Cells[row, 1].Value = fund.Building?.Name;
                        worksheet.Cells[row, 2].Value = fund.Fund1;
                        worksheet.Cells[row, 3].Value = fund.FundSource;
                        worksheet.Cells[row, 4].Value = fund.Description;
                        row++;
                    }

                    // Auto fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Return the Excel file as a byte array
                    return package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Failed to export accounts. Reason: {ex.Message}");
                throw; // Propagate the exception to the caller
            }
        }
    }
}
