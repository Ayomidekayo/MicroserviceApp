using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseServicee _baseServicee;

        public CouponService(IBaseServicee baseServicee)
        {
            this._baseServicee = baseServicee;
        }
        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto coupon)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data=coupon,
                Url = SD.CouponApiBase + "/api/coupon" 
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
           return await _baseServicee.SendAsync(new RequestDto() 
           {
               ApiType=SD.ApiType.GET,
             Url=SD.CouponApiBase+ "/api/coupon/GeetByCode/"+couponCode,
           });
        }

        public async Task<ResponseDto?> GetCouponsAsync()
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon" 
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon)
        {
            return await _baseServicee.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = coupon,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }
    }
}
