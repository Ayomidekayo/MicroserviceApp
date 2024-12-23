using Mango.Services.AuthAPI.Model;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IJWTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string>role);
    }
}
