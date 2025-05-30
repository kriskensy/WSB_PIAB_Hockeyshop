using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.PortalWWW.Controllers.Orders
{
    public class InvoicesController : Controller
    {
        private readonly HockeyshopContext _context;

        public InvoicesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.Invoices.Include(i => i.Order).Include(i => i.User);
            return View("~/Views/Orders/Invoices/Index.cshtml", await hockeyshopContext.ToListAsync());
        }

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
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.IdInvoice == id);
        }
    }
}
