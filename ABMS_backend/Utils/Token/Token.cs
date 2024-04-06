using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using System.IdentityModel.Tokens.Jwt;

namespace ABMS_backend.Utils.Token
{
    public class Token
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Token(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetUserFromToken(string token)
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
    }
}
