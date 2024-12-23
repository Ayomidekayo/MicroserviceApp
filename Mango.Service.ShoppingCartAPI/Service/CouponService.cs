using Mango.Service.ShoppingCartAPI.Model.DTO;
using Mango.Service.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Service.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponcode)
        {
            var cilent = _httpClientFactory.CreateClient("coupon");
            var respondse=await cilent.GetAsync($"/api/coupon/GetByCode/{couponcode}");
            var apiContext=await respondse.Content.ReadAsStringAsync();
            var resp=JsonConvert.DeserializeObject<ResponseDto>(apiContext);
            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
