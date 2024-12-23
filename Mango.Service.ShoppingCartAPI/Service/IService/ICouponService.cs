using Mango.Service.ShoppingCartAPI.Model.DTO;

namespace Mango.Service.ShoppingCartAPI.Service.IService
{
    public interface ICouponService 
    { 
        Task<CouponDto> GetCoupon(string couponcode);
    }
}
