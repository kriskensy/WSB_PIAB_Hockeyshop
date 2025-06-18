//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Hockeyshop.Data.Data;
//using Hockeyshop.Data.Data.Products;

//namespace Hockeyshop.PortalWWW.Controllers.Products
//{
//    public class ProductCategoriesController : BaseController
//    {
//        private readonly HockeyshopContext _context;

//        public ProductCategoriesController(HockeyshopContext context)
//        {
//            _context = context;
//        }

//        // GET: ProductCategories
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.ProductCategories.ToListAsync());
//        }

//        // GET: ProductCategories/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var productCategory = await _context.ProductCategories
//                .FirstOrDefaultAsync(m => m.IdProductCategory == id);
//            if (productCategory == null)
//            {
//                return NotFound();
//            }

//            return View(productCategory);
//        }

//        private bool ProductCategoryExists(int id)
//        {
//            return _context.ProductCategories.Any(e => e.IdProductCategory == id);
//        }
//    }
//}


using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Hockeyshop.PortalWWW.Controllers.Products
{
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }

        private bool ProductCategoryExists(int id)
        {
            return _categoryService.Exists(id);
        }
    }
}
