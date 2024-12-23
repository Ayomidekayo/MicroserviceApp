using Mango.Services.AuthAPI.Model.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            this._authService = authService;
            _responseDto= new ResponseDto();
        }
        [HttpPost("Register")]
        public async  Task< IActionResult> Register([FromBody] RegistrationRequestDTO registration)
        {
            var errorMessage = await _authService.Register(registration);
            if (!string.IsNullOrEmpty( errorMessage ))
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message=errorMessage;
              return BadRequest(_responseDto); 
            }
            return Ok(_responseDto);
        }
        [HttpPost("Login")]
        public async Task< IActionResult> Login([FromBody] LoginRequestDto loginResponseDto)
        {
            var loginResponse = await _authService.Login(loginResponseDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Usernae or Password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result= loginResponse;
            return Ok(_responseDto);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AddRole([FromBody] RegistrationRequestDTO registration)
        {
            var assignSuccessfully=await _authService.AssignRole(registration.Email,registration.Role.ToUpper());
            if (!assignSuccessfully)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto) ;
            }
            return Ok(_responseDto);
        }
    }
}
