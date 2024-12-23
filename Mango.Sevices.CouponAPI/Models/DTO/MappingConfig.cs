using AutoMapper;

namespace Mango.Sevices.CouponAPI.Models.DTO
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMap()
        {
            var mapperingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<CouponDto, Coupon>();
            });
            return mapperingConfig; 
        }
    }
}
