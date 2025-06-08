using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.PortalWWW.Controllers.Products
{
    public class ProductsController : BaseController
    {
        private readonly HockeyshopContext _context;

        public ProductsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var hockeyshopContext = _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImages);
            return View("~/Views/Products/Products/Index.cshtml", await hockeyshopContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id, int? categoryId)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.IdProduct == id);

            if (product == null)
            {
                return NotFound();
            }

            //przekazanie id kategorii do widoku
            ViewBag.CategoryId = categoryId ?? product.IdProductCategory;

            return View("~/Views/Products/Products/Details.cshtml", product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
