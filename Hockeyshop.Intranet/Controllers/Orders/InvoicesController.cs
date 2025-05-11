using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.Intranet.Controllers.Orders
{
    public class InvoicesController : Controller
    {
        private readonly HockeyshopContext _context;

        public InvoicesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Invoices.Include(i => i.Order).Include(i => i.User).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.InvoiceNumber.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Orders/Invoices/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var hockeyshopContext = _context.Invoices.Include(i => i.Order).Include(i => i.User);
        //    return View("~/Views/Orders/Invoices/Index.cshtml", await hockeyshopContext.ToListAsync());
        //}

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Order)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/Invoices/Details.cshtml", invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City");
            return View("~/Views/Orders/Invoices/Create.cshtml");
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,IdOrder,InvoiceNumber,IssueDate,TotalAmount,IdUser")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", invoice.IdOrder);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", invoice.IdUser);
            return View("~/Views/Orders/Invoices/Create.cshtml", invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", invoice.IdOrder);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", invoice.IdUser);
            return View("~/Views/Orders/Invoices/Edit.cshtml", invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInvoice,IdOrder,InvoiceNumber,IssueDate,TotalAmount,IdUser")] Invoice invoice)
        {
            if (id != invoice.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.IdInvoice))
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
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", invoice.IdOrder);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", invoice.IdUser);
            return View("~/Views/Orders/Invoices/Edit.cshtml", invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Order)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/Invoices/Delete.cshtml", invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.IdInvoice == id);
        }
    }
}
