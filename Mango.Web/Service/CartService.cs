using Mango.Web.Models;
using Mango.Web.Models.DTO;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseServicee _baseService;

        public CartService(IBaseServicee baseService)
        {
            this._baseService = baseService;
        }
        public  async Task<ResponseDto?> ApplyCartAync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoopingCartAPIBase + "/api/Cart/ApplyCoupon"
            });
        }

        public  async  Task<ResponseDto?> GetCartByUserAsnc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoopingCartAPIBase + "/api/Cart/GetCart/" + userId
            });
        }

        public async Task<ResponseDto?> RemoveCartAync(int CartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = CartDetailsId,
                Url = SD.ShoopingCartAPIBase + "/api/Cart/removeCart"
            });
        }

        public async Task<ResponseDto?> UpsertCartAync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoopingCartAPIBase + "/api/Cart/CartUpSert"
            });
        }
    }
}
