using Mango.Web.Models;
using Mango.Web.Models.AuthApiDto;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> RegisterationAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
    }
}
