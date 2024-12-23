using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Model.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTokenGenerator _jWTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        public AuthService(UserManager<ApplicationUser>  userManager,IJWTokenGenerator jWTokenGenerator,
            RoleManager<IdentityRole> roleManager,
            AppDbContext appDbContext)
        {
            this._userManager = userManager;
            this._jWTokenGenerator = jWTokenGenerator;
            this._roleManager = roleManager;
            this._appDbContext = appDbContext;
        }

        public async Task<bool> AssignRole(string emaail, string roleName)
        {
            var user=_appDbContext.ApplicationUsers.FirstOrDefault(x=>x.Email.ToLower()==emaail.ToLower());
            if (user!=null)
            {//Check if rolle exist
               if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Ccreate if role does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
               await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user=_appDbContext.ApplicationUsers.FirstOrDefault(x=>x.UserName.ToLower()==loginRequest.userName.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, loginRequest.password);
            if(user==null ||isValid==false)
            {
                return new LoginResponseDto()
                {
                    Token ="",
                    User=null,
                };
                
            };
            //If user was found, generate Jwt Token
            var role=await _userManager.GetRolesAsync(user);
            var token = _jWTokenGenerator.GenerateToken(user,role);
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token,
            };
            return loginResponseDto;
        }

        public async Task<String> Register(RegistrationRequestDTO requestDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName=requestDTO.Email,
                Name = requestDTO.Name,
                NormalizedUserName = requestDTO.Name.ToUpper(),
                Email = requestDTO.Email,
                PhoneNumber = requestDTO.PhoneNumber,
            };
            try
            {
                var result=await _userManager.CreateAsync(user,requestDTO.password);
                if(result.Succeeded)
                {
                    var returnUser=_appDbContext.ApplicationUsers.First(x=>x.UserName==requestDTO.Email);
                    UserDto userdto = new ()
                    {
                        Id = returnUser.Id,
                        Email = returnUser.Email,
                        PhoneNumber = returnUser.PhoneNumber,
                        Name = returnUser.Name,
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

               

            }
            return "Error encountered";

        }
    }
}
