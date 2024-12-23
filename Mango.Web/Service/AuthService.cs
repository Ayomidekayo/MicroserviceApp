using Mango.Web.Models;
using Mango.Web.Models.AuthApiDto;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseServicee _baseServicee;

        public AuthService(IBaseServicee baseServicee)
        {
            this._baseServicee = baseServicee;
        }
        public async  Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDTO,
                Url = SD.AuthApiBase + "/api/Auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthApiBase + "/api/Auth/Login"
            },withBearer:false);
        }

        public async Task<ResponseDto?> RegisterationAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDTO,
                Url = SD.AuthApiBase + "/api/Auth/Register"
            },withBearer:false);
        }
    }
}
