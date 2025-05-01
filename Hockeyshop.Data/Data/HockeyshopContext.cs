using Hockeyshop.Data.Data.Cart;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.Data.Data.Inventory;
using Hockeyshop.Data.Data.Marketing;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Data.Data.Payments;
using Hockeyshop.Data.Data.Products;
using Microsoft.EntityFrameworkCore;


namespace Hockeyshop.Data.Data
{
    public class HockeyshopContext : DbContext
    {
        public HockeyshopContext(DbContextOptions<HockeyshopContext> options) 
            : base(options)
        {
        }

        //Cart
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<UserCart> UserCarts { get; set; } = default!;

        //CMS
        public DbSet<HockeyNews> HockeyNews { get; set; } = default!;

        //Core
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;

        //Inventory
        public DbSet<Stock> Stocks { get; set; } = default!;

        //Marketing
        public DbSet<DiscountType> DiscountTypes { get; set; } = default!;
        public DbSet<ProductPromotion> ProductPromotions { get; set; } = default!;
        public DbSet<Promotion> Promotions { get; set; } = default!;


        //Orders
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = default!;

        //Payments
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<PaymentStatus> PaymentStatuses { get; set; } = default!;

        //Products
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = default!;
        public DbSet<ProductImage> ProductImages { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductPromotionConfiguration).Assembly);
        }
    }
}
