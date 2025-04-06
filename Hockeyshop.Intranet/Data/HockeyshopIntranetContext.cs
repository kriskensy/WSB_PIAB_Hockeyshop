using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Intranet.Models.Documents;
using Hockeyshop.Intranet.Models.Help;
using Hockeyshop.Intranet.Models.CMS;
using Hockeyshop.Intranet.Models.Orders;
using Hockeyshop.Intranet.Models.Inventory;
using Hockeyshop.Intranet.Models.Support;
using Hockeyshop.Intranet.Models.Management;

namespace Hockeyshop.Intranet.Data
{
    public class HockeyshopIntranetContext : DbContext
    {
        public HockeyshopIntranetContext (DbContextOptions<HockeyshopIntranetContext> options)
            : base(options)
        {
        }

        public DbSet<Hockeyshop.Intranet.Models.Documents.Document> Document { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Documents.DocumentCategory> DocumentCategory { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Help.FaqCategory> FaqCategory { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Help.FaqEntry> FaqEntry { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.CMS.HockeyNews> HockeyNews { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Orders.Order> Order { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Orders.OrderItem> OrderItem { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Inventory.Product> Product { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Inventory.ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Inventory.Stock> Stock { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Inventory.Supplier> Supplier { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Support.Ticket> Ticket { get; set; } = default!;
        public DbSet<Hockeyshop.Intranet.Models.Management.User> User { get; set; } = default!;
    }
}
