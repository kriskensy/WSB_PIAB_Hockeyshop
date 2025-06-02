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
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.IdProductCategory).ToListAsync();

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

        // GET: Shop/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var query = _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImages)
                .AsQueryable();

            //szukawanie w rekordach tabel
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.ProductCategory.Name.Contains(searchTerm) ||
                    item.Supplier.Name.Contains(searchTerm) ||
                    item.Description.Contains(searchTerm) ||
                    item.Name.Contains(searchTerm));

                ViewBag.CurrentCategoryName = $"Search results for: \"{searchTerm}\"";
            }
            else
            {
                ViewBag.CurrentCategoryName = "All products";
            }

            var products = await query.OrderBy(p => p.Name).ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Products/Products/Index.cshtml", products);
        }

        public async Task<IActionResult> OurHighlights()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var highlightedProducts = await _context.Products
                .Include (p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImages)
                .Where(p => p.Highlight)
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.CurrentCategoryName = "Our Highlights";

            return View("~/Views/Products/Products/Index.cshtml", highlightedProducts);
        }

        //public async Task<IActionResult> NewArrivals()
        //{
        //    ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

        //    var highlightedProducts = await _context.Products
        //        .Include(p => p.ProductCategory)
        //        .Include(p => p.Supplier)
        //        .Include(p => p.ProductImages)
        //        .Where(p => p.NewArrival)
        //        .OrderBy(p => p.Name)
        //        .ToListAsync();

        //    ViewBag.CurrentCategoryName = "New Arrivals";

        //    return View("~/Views/Products/Products/Index.cshtml", highlightedProducts);
        //}
    }
}