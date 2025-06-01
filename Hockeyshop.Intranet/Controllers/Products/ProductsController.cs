using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Extensions;

namespace Hockeyshop.Intranet.Controllers.Products
{
    public class ProductsController : Controller
    {
        private readonly HockeyshopContext _context;

        public ProductsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImages)
                .AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.ProductCategory.Name.Contains(searchTerm) ||
                    item.Supplier.Name.Contains(searchTerm) ||
                    item.Description.Contains(searchTerm) ||
                    item.Name.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Products/Products/Index.cshtml", model);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View("~/Views/Products/Products/Details.cshtml", product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.IdProductCategory = new SelectList(_context.ProductCategories, "IdProductCategory", "Name");
            ViewBag.IdSupplier = new SelectList(_context.Suppliers, "IdSupplier", "Name");
            return View("~/Views/Products/Products/Create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Name,IdProductCategory,Price,IdSupplier, Highlight, NewArrival, Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdProductCategory = new SelectList(_context.ProductCategories, "IdProductCategory", "Name", product.IdProductCategory);
            ViewBag.IdSupplier = new SelectList(_context.Suppliers, "IdSupplier", "Name", product.IdSupplier);
            return View("~/Views/Products/Products/Create.cshtml", product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.IdProductCategory = new SelectList(_context.ProductCategories, "IdProductCategory", "Name", product.IdProductCategory);
            ViewBag.IdSupplier = new SelectList(_context.Suppliers, "IdSupplier", "Name", product.IdSupplier);
            return View("~/Views/Products/Products/Edit.cshtml", product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Name,IdProductCategory,Price,IdSupplier, Highlight, NewArrival, Description")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
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
            ViewBag.IdProductCategory = new SelectList(_context.ProductCategories, "IdProductCategory", "Name", product.IdProductCategory);
            ViewBag.IdSupplier = new SelectList(_context.Suppliers, "IdSupplier", "Name", product.IdSupplier);
            return View("~/Views/Products/Products/Edit.cshtml", product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/Products/Delete.cshtml", product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
