using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.PortalWWW.Controllers.Orders
{
    public class InvoicesController : BaseController
    {
        private readonly HockeyshopContext _context;

        public InvoicesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Invoices
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var invoices = await _context.Invoices
                .Where(i => i.IdUser == userId)
                .Include(i => i.Order)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();

            return View("~/Views/Orders/Invoices/Index.cshtml", invoices);
        }


        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Order)
                    .ThenInclude(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
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
