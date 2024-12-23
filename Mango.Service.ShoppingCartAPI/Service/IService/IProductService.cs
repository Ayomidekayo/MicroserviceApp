using Mango.Service.ShoppingCartAPI.Model.DTO;

namespace Mango.Service.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
       Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
