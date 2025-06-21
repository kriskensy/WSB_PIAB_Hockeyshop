using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Services.Products
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(HockeyshopContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
