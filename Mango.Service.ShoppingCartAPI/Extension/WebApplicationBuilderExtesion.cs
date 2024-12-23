using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Service.ShoppingCartAPI.Extension
{
    public static class WebApplicationBuilderExtesion
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var secreate = builder.Configuration.GetValue<string>("ApiSettingS:Secrete");
            var audience = builder.Configuration.GetValue<string>("ApiSettingS:Audience");
            var issuer = builder.Configuration.GetValue<string>("ApiSettingS:Issuer");
            var key = Encoding.ASCII.GetBytes(secreate);
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                };
            });

            return builder;
        }
    }
}
