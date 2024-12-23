using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mango.Services.ProductAPI.Extension
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder) 
        {
            var secrete = builder.Configuration.GetValue<string>("ApiSettingS:Secrete");
            var audience = builder.Configuration.GetValue<string>("ApiSettingS:Audience");
            var issuer = builder.Configuration.GetValue<string>("ApiSettingS:Issuer");
            var key=Encoding.ASCII.GetBytes(secrete);

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            return builder;
        }
    }
}
