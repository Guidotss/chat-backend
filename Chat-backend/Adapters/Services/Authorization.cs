using Chat_backend.Interfaces;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;

namespace Chat_backend.Adapters.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IConfiguration _configuration; 
        public Authorization(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public string GetEmail(string token)
        {
            var key = _configuration.GetValue<string>("JWTSECRET");
            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var claims = new ClaimsIdentity();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                return email;
            }
            catch
            {
                return "";
            }
        }
        public string GetToken(string email, string username, Guid id)
        {
            DateTime createdAt = DateTime.UtcNow;
            var key = _configuration.GetValue<string>("JWTSECRET"); 
            var keyBytes = Encoding.ASCII.GetBytes(key!);

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Email, email));
            claims.AddClaim(new Claim(ClaimTypes.Name, username));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = createdAt.AddHours(2),
                SigningCredentials = credentialsToken
            }; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = new JwtSecurityTokenHandler().CreateToken(tokenDescription);

            string token = tokenHandler.WriteToken(tokenConfig);


            return token; 
            
        }

        public bool VerifyToken(string token)
        {
            var key = _configuration.GetValue<string>("JWTSECRET"); 
            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var claims = new ClaimsIdentity();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };


            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true; 
            }
            catch
            {
                return false;
            }
        }
    }
}
