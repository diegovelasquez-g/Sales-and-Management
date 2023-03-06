using Microsoft.IdentityModel.Tokens;
using Sales_and_Management.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Sales_and_Management.Security
{
    public class AuthValidator
    {
        private readonly IConfiguration _configuration;
        public AuthValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwt.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(_ => _.Type == "id").Value);

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
