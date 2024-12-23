using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IBaseServicee
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto,bool withBearer=true);
    }
}
