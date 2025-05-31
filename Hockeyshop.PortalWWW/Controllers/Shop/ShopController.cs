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

        //do zwracania randomowych produktów z promocji na stronę główną sklepu
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var randomPromotedProducts = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => _context.ProductPromotions.Any(pp => pp.IdProduct == p.IdProduct))
                .OrderBy(p => Guid.NewGuid())
                .Take(9)
                .ToListAsync();

            return View(randomPromotedProducts);
        }

        public async Task<IActionResult> ProductCategory(int id)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var category = await _context.ProductCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdProductCategory == id);

            var products = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.IdProductCategory == id)
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.CurrentCategoryName = category?.Name ?? "Unknown category";

            return View("~/Views/Products/Products/Index.cshtml", products);
        }

    }
}
