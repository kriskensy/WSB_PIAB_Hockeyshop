using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Payments;

namespace Hockeyshop.PortalWWW.Controllers.Payments
{
    public class PaymentsController : Controller
    {
        private readonly HockeyshopContext _context;

        public PaymentsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.Payments.Include(p => p.Order).Include(p => p.PaymentMethod).Include(p => p.PaymentStatus);
            return View(await hockeyshopContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Order)
                .Include(p => p.PaymentMethod)
                .Include(p => p.PaymentStatus)
                .FirstOrDefaultAsync(m => m.IdPayment == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.IdPayment == id);
        }
    }
}
