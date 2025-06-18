using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Intranet.Controllers.Products
{
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _categoryService.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/Products/ProductCategories/Index.cshtml", model);
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _categoryService.GetByIdAsync(id.Value);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/ProductCategories/Details.cshtml", productCategory);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View("~/Views/Products/ProductCategories/Create.cshtml");
        }

        // POST: ProductCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductCategory,Name")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(productCategory);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Products/ProductCategories/Create.cshtml", productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _categoryService.GetByIdAsync(id.Value);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View("~/Views/Products/ProductCategories/Edit.cshtml", productCategory);
        }

        // POST: ProductCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductCategory,Name")] ProductCategory productCategory)
        {
            if (id != productCategory.IdProductCategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(productCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.IdProductCategory))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Products/ProductCategories/Edit.cshtml", productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _categoryService.GetByIdAsync(id.Value);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/ProductCategories/Delete.cshtml", productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }

        private bool ProductCategoryExists(int id)
        {
            return _categoryService.Exists(id);
        }
    }
}
