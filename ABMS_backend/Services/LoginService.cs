using ABMS_backend.Models;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ABMS_backend.Utils.Validates;
using System.Net;
using Newtonsoft.Json;
using ABMS_backend.Utils.Exceptions;
using OfficeOpenXml;
using ABMS_backend.Utils.Token;
using Microsoft.EntityFrameworkCore;

namespace ABMS_backend.Services
{
    public class LoginService : ILoginAccount
    {
        private readonly abmsContext _abmsContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(abmsContext abmsContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _abmsContext = abmsContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserFromToken(string token)
        {
            if (token == null)
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

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghiklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ResponseData<string> ResetPassword(string id)
        {
            Account a = _abmsContext.Accounts.Find(id);
            try
            {
                var new_password = RandomString(8);
                HashPasword(new_password, out byte[] passwordHash, out byte[] passwordSalt);
                //string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                a.PasswordHash = passwordHash;
                a.PasswordSalt = passwordSalt;
                //a.ModifyUser = getUser;
                a.ModifyTime = DateTime.Now;
                _abmsContext.Update(a);
                _abmsContext.SaveChanges();
                return new ResponseData<string>
                {
                    Data = new_password,
                    StatusCode = HttpStatusCode.OK,
                    ErrMsg = ErrorApp.SUCCESS.description
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Reset password failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> ChangePassword(string id, ChangePassword password)
        {
            Account a = _abmsContext.Accounts.Find(id);
            //validate
            string error = password.Validate();
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
                if (VerifyPassword(password.old_password, a.PasswordHash, a.PasswordSalt))
                {
                    if (password.new_password == password.old_password)
                    {
                        return new ResponseData<string>
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            ErrMsg = "Must be new password!"
                        };
                    }

                    HashPasword(password.new_password, out byte[] passwordHash, out byte[] passwordSalt);
                    string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);

                    a.PasswordHash = passwordHash;
                    a.PasswordSalt = passwordSalt;
                    a.ModifyUser = getUser;
                    a.ModifyTime = DateTime.Now;

                    _abmsContext.Accounts.Update(a);
                    _abmsContext.SaveChanges();
                    return new ResponseData<string>
                    {
                        Data = a.Id,
                        StatusCode = HttpStatusCode.OK,
                        ErrMsg = ErrorApp.SUCCESS.description
                    };
                }
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Wrong old password!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Change password failed: " + ex.Message
                };
            }
        }

        public ResponseData<string> GetAccount(Login dto)
        {
            var account = _abmsContext.Accounts.FirstOrDefault(x => x.PhoneNumber == dto.phoneNumber);
            if (account != null)
            {
                if (VerifyPassword(dto.password, account.PasswordHash, account.PasswordSalt))
                {
                    string token = GetToken(account);
                    _httpContextAccessor.HttpContext.Session.SetString("user", account.UserName);
                    _httpContextAccessor.HttpContext.Session.SetInt32("role", account.Role);
                    return new ResponseData<string>
                    {
                        Data = token,
                        StatusCode = HttpStatusCode.OK,
                        ErrMsg = ErrorApp.SUCCESS.description
                    };
                }
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Wrong password!"
                };
            }
            return new ResponseData<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrMsg = "User not found!"
            };
        }

        public ResponseData<string> GetAccountByEmail(LoginWithEmail dto)
        {
            var account = _abmsContext.Accounts.FirstOrDefault(x => x.Email == dto.email);
            if (account != null)
            {
                if (VerifyPassword(dto.password, account.PasswordHash, account.PasswordSalt))
                {
                    string token = GetToken(account);
                    return new ResponseData<string>
                    {
                        Data = token,
                        StatusCode = HttpStatusCode.OK,
                        ErrMsg = ErrorApp.SUCCESS.description
                    };
                }
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Wrong password!"
                };
            }
            return new ResponseData<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrMsg = "User not found!"
            };
        }

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }

        string HashPasword(string password, out byte[] hash, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        public string GetToken(Account dto)
        {
            var user = _abmsContext.Accounts.Where(u => u.PhoneNumber == dto.PhoneNumber).FirstOrDefault();
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("Role", user.Role.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
                 new Claim("User", user.UserName.ToString()),
                 new Claim("Email", user.Email),
                 new Claim("PhoneNumber", user.PhoneNumber.ToString()),
                 new Claim("FullName", user.FullName),
                 new Claim("BuildingId", user.BuildingId),
                 new Claim("Status", user.Status.ToString()),
                 //new Claim("Id", dto.Id),
                 new Claim("Id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn
                );

            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }


        public ResponseData<string> Register(RegisterDTO request)
        {
            var checkExist = _abmsContext.Accounts.FirstOrDefault(x => x.PhoneNumber == request.phone || x.Email == request.email);
            if (checkExist != null)
            {
                return new ResponseData<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrMsg = "Account existed!"
                };
            }

            HashPasword(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            string getUser = GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            Account user = new Account
            {
                Email = request.email,
                PhoneNumber = request.phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FullName = request.full_name,
                BuildingId = request.building_id,
                Role = request.role,
                UserName = request.user_name,
                CreateUser = getUser,
                CreateTime = DateTime.Now,
                Status = (int)Utils.Validates.Constants.STATUS.ACTIVE,
                Id = Guid.NewGuid().ToString()
                //Role = "customer"
            };
            _abmsContext.Accounts.Add(user);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = user.Id,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }

        public ResponseData<string> ImportData(IFormFile file, int role, string buildingId)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return new ResponseData<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrMsg = "File not selected or empty."
                    };
                }
                List<String> list = new List<String>();
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            return new ResponseData<string>
                            {
                                StatusCode = HttpStatusCode.BadRequest,
                                ErrMsg = "Excel file is empty or malformed."
                            };
                        }

                        int rowCount = worksheet.Dimension.Rows;
                        if (rowCount < 2)
                        {
                            return new ResponseData<string>
                            {
                                StatusCode = HttpStatusCode.BadRequest,
                                ErrMsg = "Excel file must contain at least one row of data."
                            };
                        }


                        for (int row = 2; row <= rowCount; row++) // Assuming the first row is header
                        {
                            Account account = new Account();
                            account.Id = Guid.NewGuid().ToString();
                            //string buildingName = worksheet.Cells[row, 1].Value?.ToString();
                            //Building building = _abmsContext.Buildings.FirstOrDefault(x => x.Name == buildingName);
                            account.BuildingId = buildingId;
                            account.PhoneNumber = worksheet.Cells[row, 1].Value?.ToString();
                            string password = worksheet.Cells[row, 2].Value?.ToString();
                            HashPasword(password, out byte[] passwordHash, out byte[] passwordSalt);
                            account.PasswordSalt = passwordSalt;
                            account.PasswordHash = passwordHash;
                            account.UserName = worksheet.Cells[row, 3].Value?.ToString();
                            account.Email = worksheet.Cells[row, 4].Value?.ToString();
                            account.FullName = worksheet.Cells[row, 5].Value?.ToString();
                            account.Role = role;
                            string getUser = Token.GetUserFromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                            account.CreateUser = getUser;
                            account.CreateTime = DateTime.Now;
                            account.Status = (int)Utils.Validates.Constants.STATUS.ACTIVE;
                            _abmsContext.Accounts.Add(account);
                            list.Add(account.Id);
                        }
                        _abmsContext.SaveChanges();
                    }
                }
                string jsonData = JsonConvert.SerializeObject(list);
                return new ResponseData<string>
                {
                    Data = jsonData,
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

        public byte[] ExportData(string buildingId)
        {
            try
            {
                var accounts = _abmsContext.Accounts.Include(x => x.Building).Where(x => x.Role == (int)Utils.Validates.Constants.ROLE.ROOM && x.BuildingId == buildingId).ToList();
                Building building = _abmsContext.Buildings.Find(buildingId);
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Accounts");

                    // Add title
                    var titleCell = worksheet.Cells["A1:D1"];
                    titleCell.Merge = true; // Merge cells from A1 to F1
                    titleCell.Value = building.Name + "'s Resident Account Statistics";
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.Font.Size = 20;

                    // Add headers
                    worksheet.Cells["A3"].Value = "Phone Number";
                    worksheet.Cells["B3"].Value = "User Name";
                    worksheet.Cells["C3"].Value = "Email";
                    worksheet.Cells["D3"].Value = "Full Name";
                    worksheet.Cells["E3"].Value = "Room";
                    worksheet.Cells["F3"].Value = "Residents";

                    worksheet.Cells["A3"].Style.Font.Bold = true;
                    worksheet.Cells["B3"].Style.Font.Bold = true;
                    worksheet.Cells["C3"].Style.Font.Bold = true;
                    worksheet.Cells["D3"].Style.Font.Bold = true;
                    worksheet.Cells["E3"].Style.Font.Bold = true;
                    worksheet.Cells["F3"].Style.Font.Bold = true;

                    // Add data
                    int row = 4;
                    Room room = new Room();
                    foreach (var account in accounts)
                    {
                        worksheet.Cells[row, 1].Value = account.PhoneNumber;
                        worksheet.Cells[row, 2].Value = account.UserName;
                        worksheet.Cells[row, 3].Value = account.Email;
                        worksheet.Cells[row, 4].Value = account.FullName;
                        room = _abmsContext.Rooms.FirstOrDefault(x => x.AccountId == account.Id);

                        if (room != null)
                        {
                            worksheet.Cells[row, 5].Value = room.RoomNumber; // Cột 5: Số phòng

                            var residents = _abmsContext.Residents.Where(r => r.RoomId == room.Id).Select(r => r.FullName).ToList();
                            string residentsText = string.Join("\n", residents); // Chuỗi các cư dân, mỗi cư dân trên một dòng

                            // Gán văn bản với định dạng xuống dòng cho mỗi cư dân
                            worksheet.Cells[row, 6].Value = residentsText;
                            worksheet.Cells[row, 6].Style.WrapText = true;
                        }
                        // Tăng row khi ghi mỗi tài khoản
                        row++;
                    }
                    worksheet.Column(6).Width = 20;

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
