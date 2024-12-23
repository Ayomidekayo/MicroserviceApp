using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Service
{
    public class JWTokenGenerator : IJWTokenGenerator
    {
        private readonly JWTOptions _jWTOptions;

        public JWTokenGenerator(IOptions< JWTOptions> jWTOptions)
        {
            this._jWTOptions = jWTOptions.Value;
        }
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
                var key=Encoding.ASCII.GetBytes(_jWTOptions.Secrete);
                var Clamlist = new  List<Claim>()
            {
              new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
              new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
              new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
            };
            Clamlist.AddRange(role.Select(roles => new Claim(ClaimTypes.Role, roles)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jWTOptions.Audience,
                Issuer = _jWTOptions.Issuer,
                Subject = new ClaimsIdentity(Clamlist),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
           
        }
    }
}
