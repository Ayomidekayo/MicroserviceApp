using Mango.Services.AuthAPI.Model.DTO;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO requestDTO);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
        Task<bool> AssignRole(string emaail, string roleName);
        
    }
}
