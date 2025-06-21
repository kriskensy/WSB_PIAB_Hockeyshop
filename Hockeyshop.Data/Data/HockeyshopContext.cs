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
        public DbSet<IconLibrary> IconLibraries { get; set; } = default!;
        public DbSet<ShopRule> ShopRules { get; set; } = default!;
        public DbSet<WelcomeText> WelcomeTexts { get; set; }
        public DbSet<ContactSection> ContactSections { get; set; }
        public DbSet<FooterSection> FooterSections { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

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

            //dla cykli kaskadowych
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.IdUserRole)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany()
                .HasForeignKey(p => p.IdProductCategory)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.IdSupplier)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.IdOrderStatus)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.IdOrder)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.IdProduct)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.IdOrder)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey(p => p.IdPaymentMethod)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentStatus)
                .WithMany()
                .HasForeignKey(p => p.IdPaymentStatus)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.IdProduct)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.UserCart)
                .WithMany()
                .HasForeignKey(ci => ci.IdUserCart)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserCart>()
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.IdProduct)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<ProductPromotion>()
            //    .HasOne(pp => pp.Product)
            //    .WithMany()
            //    .HasForeignKey(pp => pp.IdProduct)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<ProductPromotion>()
            //    .HasOne(pp => pp.Promotion)
            //    .WithMany()
            //    .HasForeignKey(pp => pp.IdPromotion)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShopRule>()
                .HasOne(s => s.IconLibrary)
                .WithMany()
                .HasForeignKey(s => s.IdIcon)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductPromotionConfiguration).Assembly);
        }
    }
}
