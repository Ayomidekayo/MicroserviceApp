using Mango.Sevices.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Sevices.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Coupon>().HasData(new List<Coupon>
            {
                new Coupon()
                {
                    CouponId=1,
                    CouponCode="100F",
                    DiscountAmount=10,
                    MinAmount=20,
                },
                new Coupon()
                {
                    CouponId=2,
                    CouponCode="200F",
                    DiscountAmount=20,
                    MinAmount=40,
                },
                
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
