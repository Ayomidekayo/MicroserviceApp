using System.ComponentModel.DataAnnotations;

namespace Mango.Sevices.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public Double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
