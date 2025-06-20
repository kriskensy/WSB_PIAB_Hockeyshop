using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Interfaces.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hockeyshop.PortalWWW.Controllers.Orders
{
    public class InvoicesController : BaseController
    {
        private readonly HockeyshopContext _context;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(HockeyshopContext context, IInvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        // GET: Invoices
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var invoices = await _invoiceService.GetForUserAsync(userId);

            return View("~/Views/Orders/Invoices/Index.cshtml", invoices);
        }

        // GET: Invoices/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            if (id == null)
                return NotFound();

            var invoice = await _invoiceService.GetByIdAsync(id.Value);
            if (invoice == null)
                return NotFound();

            return View("~/Views/Orders/Invoices/Details.cshtml", invoice);
        }

        // GET: Invoices/DownloadPdf/5
        [Authorize]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            //sprawdzenie czy fakture jest od usera
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var invoice = await _invoiceService.GetByIdAsync(id);

            if (invoice == null || invoice.IdUser != userId)
                return NotFound();

            var pdfBytes = await _invoiceService.GeneratePdfAsync(id);
            return File(pdfBytes, "application/pdf", $"Invoice-{invoice.InvoiceNumber}.pdf");
        }

        private bool InvoiceExists(int id)
        {
            return _invoiceService.Exists(id);
        }
    }
}
