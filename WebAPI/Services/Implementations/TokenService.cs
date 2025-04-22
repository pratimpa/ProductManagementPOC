using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services
{
    public class TokenService : ITokenService
    {
        IConfiguration config;
        public TokenService(IConfiguration config)
        {
            this.config = config;
        }
        public string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int ExpireMinutes = Convert.ToInt32(config["Jwt:ExpireMinutes"]);

            var claims = new[] {
                             new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
                             new Claim("Roles", string.Join(",",user.Roles)),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                                            config["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.UtcNow.AddMinutes(ExpireMinutes), //token expiry minutes
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
