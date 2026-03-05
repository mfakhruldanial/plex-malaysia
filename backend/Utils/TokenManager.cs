using Azure.Core;
using backend.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace backend.Utils
{
    public class TokenManager
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenManager(
            IConfiguration config)
        {
            _key = config["JWT:Key"]!;
            _issuer = config["JWT:Issuer"]!;
            _audience = config["JWT:Audience"]!;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.IdRolNavigation!.Name!),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}")
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void CreateCookie(HttpContext httpContext, string token)
        {
            httpContext.Response.Cookies.Append("AT", token, new CookieOptions
            {
                //HttpOnly = true,
                //Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1),
                Path = "/",
                Domain = "localhost"
            });
        }
    }
}
