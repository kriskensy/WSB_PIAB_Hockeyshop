using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Data.Data.Documents;
using Hockeyshop.Data.Data.Help;
using Hockeyshop.Data.Data.Inventory;
using Hockeyshop.Data.Data.Management;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Data.Data.Support;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data
{
    public class HockeyshopContext : DbContext
    {
        public HockeyshopContext(DbContextOptions<HockeyshopContext> options) 
            : base(options)
        {
        }

        public DbSet<Document> Document { get; set; } = default!;
        public DbSet<DocumentCategory> DocumentCategory { get; set; } = default!;
        public DbSet<FaqCategory> FaqCategory { get; set; } = default!;
        public DbSet<FaqEntry> FaqEntry { get; set; } = default!;
        public DbSet<HockeyNews> HockeyNews { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderItem> OrderItem { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<Stock> Stock { get; set; } = default!;
        public DbSet<Supplier> Supplier { get; set; } = default!;
        public DbSet<Ticket> Ticket { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}
