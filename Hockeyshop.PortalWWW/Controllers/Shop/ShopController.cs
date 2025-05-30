using Hockeyshop.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.PortalWWW.Controllers.Shop
{
    public class ShopController : Controller
    {
        private readonly HockeyshopContext _context;

        public ShopController(HockeyshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var randomPromotedProducts = await _context.Products
                .Where(p => _context.ProductPromotions.Any(pp => pp.IdProduct == p.IdProduct))
                .OrderBy(p => Guid.NewGuid())
                .Take(4)
                .ToListAsync();

            return View(randomPromotedProducts);
        }

        public async Task<IActionResult> ProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories
                .Include(c => c.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdProductCategory == id);

            return View(productCategory?.Products?.OrderBy(p => p.Name).ToList() ?? new());
        }
    }
}
