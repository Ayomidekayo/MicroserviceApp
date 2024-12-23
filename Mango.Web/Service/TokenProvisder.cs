using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mango.Web.Service
{
    public class TokenProvisder : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvisder(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }
        public void CearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? tokenExists = 
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return tokenExists is true?  token:null;
        }

        public void SetToken(string token)
        {
           _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie,token);
        }
    }
}
