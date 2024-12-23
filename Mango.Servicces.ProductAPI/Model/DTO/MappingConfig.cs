using AutoMapper;

namespace Mango.Servicces.ProductAPI.Model.DTO
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mapperingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });
            return mapperingConfig;
        }
    }
}
