using Mango.Web.Models;
using Mango.Web.Models.DTO;

namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserAsnc(string userId);
        Task<ResponseDto?> UpsertCartAync(CartDto cartDto);
        Task<ResponseDto?> RemoveCartAync(int CartDetailsId);
        Task<ResponseDto?> ApplyCartAync(CartDto cartDto);
    }
}
