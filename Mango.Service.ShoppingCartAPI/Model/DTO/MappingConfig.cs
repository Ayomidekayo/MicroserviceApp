using AutoMapper;

namespace Mango.Service.ShoppingCartAPI.Model.DTO
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mapperingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartDetails, CartDetailsDto>();
                config.CreateMap<CartDetailsDto, CartDetails>();

                config.CreateMap<CartHeader, CartHeaderDto>();
                config.CreateMap<CartHeaderDto, CartHeader>();

            });
            return mapperingConfig;
        }
    }
}
