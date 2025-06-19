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
