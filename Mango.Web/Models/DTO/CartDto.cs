
using Mango.Web.Models.DTO;

namespace Mango.Web.Models.DTO
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto>?  CartDetails { get; set; }
    }
}
