using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Inventory;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Inventory
{
    public class StocksController : Controller
    {
        private readonly HockeyshopContext _context;

        public StocksController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Stocks.Include(s => s.Product).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.Product.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Inventory/Stocks/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var hockeyshopContext = _context.Stocks.Include(s => s.Product);
        //    return View("~/Views/Inventory/Stocks/Index.cshtml", await hockeyshopContext.ToListAsync());
        //}

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.IdStock == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View("~/Views/Inventory/Stocks/Detials.cshtml", stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description");
            return View("~/Views/Inventory/Stocks/Create.cshtml");
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStock,IdProduct,Quantity,UpdatedAt")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", stock.IdProduct);
            return View("~/Views/Inventory/Stocks/Create.cshtml", stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", stock.IdProduct);
            return View("~/Views/Inventory/Stocks/Edit.cshtml", stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStock,IdProduct,Quantity,UpdatedAt")] Stock stock)
        {
            if (id != stock.IdStock)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.IdStock))
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
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", stock.IdProduct);
            return View("~/Views/Inventory/Stocks/Edit.cshtml", stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.IdStock == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View("~/Views/Inventory/Stocks/Delete.cshtml", stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var stock = await _context.Stocks.FindAsync(id);
                if (stock != null)
                {
                    _context.Stocks.Remove(stock);
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

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.IdStock == id);
        }
    }
}
