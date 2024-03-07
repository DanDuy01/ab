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
using Microsoft.AspNetCore.Http;
using ABMS_backend.Utils.Exceptions;

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

        ResponseData<string> ILoginAccount.getAccount(Login dto)
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

        ResponseData<string> ILoginAccount.getAccountByEmail(LoginWithEmail dto)
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
                 //new Claim("Id", dto.Id),
                 new Claim("Id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);

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
                Status = (int)Constants.STATUS.ACTIVE,
                Id = Guid.NewGuid().ToString()
                //Role = "customer"
            };
            _abmsContext.Accounts.Add(user);
            _abmsContext.SaveChanges();
            return new ResponseData<string>
            {
                Data = user.PhoneNumber,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
