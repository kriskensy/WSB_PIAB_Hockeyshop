using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Extensions;

namespace Hockeyshop.Intranet.Controllers.Products
{
    public class SuppliersController : Controller
    {
        private readonly HockeyshopContext _context;

        public SuppliersController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Suppliers.AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.ContactEmail.Contains(searchTerm) ||
                    item.PostCode.Contains(searchTerm) ||
                    item.City.Contains(searchTerm) ||
                    item.StreetAndNumber.Contains(searchTerm) ||
                    item.Name.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Products/Suppliers/Index.cshtml", model);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.IdSupplier == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/Suppliers/Details.cshtml", supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            ViewBag.IdSupplier = new SelectList(_context.Suppliers, "IdSupplier", "Name");
            return View("~/Views/Products/Suppliers/Create.cshtml");
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupplier,Name,ContactEmail,PostCode,City,StreetAndNumber")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Products/Suppliers/Create.cshtml", supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View("~/Views/Products/Suppliers/Edit.cshtml", supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSupplier,Name,ContactEmail,PostCode,City,StreetAndNumber")] Supplier supplier)
        {
            if (id != supplier.IdSupplier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.IdSupplier))
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
            return View("~/Views/Products/Suppliers/Edit.cshtml", supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.IdSupplier == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/Suppliers/Delete.cshtml", supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier != null)
                {
                    _context.Suppliers.Remove(supplier);
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

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.IdSupplier == id);
        }
    }
}
