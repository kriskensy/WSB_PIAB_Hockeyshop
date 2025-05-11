using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.Intranet.Controllers.Products
{
    public class ProductCategoriesController : Controller
    {
        private readonly HockeyshopContext _context;

        public ProductCategoriesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.ProductCategories.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pm => pm.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Products/ProductCategories/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View("~/Views/Products/ProductCategories/Index.cshtml", await _context.ProductCategories.ToListAsync());
        //}

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.IdProductCategory == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductCategory,Name")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
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

            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View("~/Views/Products/ProductCategories/Edit.cshtml", productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
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

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.IdProductCategory == id);
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
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.IdProductCategory == id);
        }
    }
}
