using Mango.Web.Models;
using Mango.Web.Models.AuthApiDto;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider tokenProvider;

        public AuthController(IAuthService authService,ITokenProvider tokenProvider)
        {
            this._authService = authService;
            this.tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto= new();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);
            if(responseDto !=null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
               //cookies  identity authentication
                await SiginUser(loginResponseDto);

                //generate token
                tokenProvider.SetToken(loginResponseDto.Token);
               return  RedirectToAction("Index","Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text =SD.ROLeAdmin,Value= SD.ROLeAdmin},
                new SelectListItem{ Text =SD.ROLeCustomer,Value= SD.ROLeCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {
            ResponseDto result = await _authService.RegisterationAsync(obj);
            ResponseDto assignRole;
            if(result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.ROLeCustomer;
                }
              assignRole=  await _authService.AssignRoleAsync(obj);
                if(assignRole !=null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registratio was Successful";
                    return RedirectToAction(nameof(Login));
                }

            }
            else
            {
                TempData["error"] = result.Message;
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text =SD.ROLeAdmin,Value= SD.ROLeAdmin},
                new SelectListItem{ Text =SD.ROLeCustomer,Value= SD.ROLeCustomer}
            };
            ViewBag.RoleList = roleList;
            return View(obj);
        }
        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.CearToken();
            return RedirectToAction("Index","Home");
        }
        public async Task SiginUser(LoginResponseDto model)
        {
            var handler=new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            //default identity
            identity.AddClaim(new Claim(ClaimTypes.Name,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            var principal=new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
      
    }
}
