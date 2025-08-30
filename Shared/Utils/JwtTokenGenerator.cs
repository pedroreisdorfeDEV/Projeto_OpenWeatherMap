using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projetoGloboClima.Shared.Utils
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string userName, string userId)
        {


            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenHandler = new JwtSecurityTokenHandler();

                var claims = new[]
                {
                 new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName),
                new Claim("userId", userId)
            };

                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = credentials
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                // tratar
                throw;
            }

        }
    }


}
