using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Payments;

namespace Hockeyshop.Intranet.Controllers.Payments
{
    public class PaymentStatusController : Controller
    {
        private readonly HockeyshopContext _context;

        public PaymentStatusController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: PaymentStatus
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.PaymentStatuses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pm => pm.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Payments/PaymentStatus/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View("~/Views/Payments/PaymentStatus/Index.cshtml", await _context.PaymentStatuses.ToListAsync());
        //}

        // GET: PaymentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatus = await _context.PaymentStatuses
                .FirstOrDefaultAsync(m => m.IdPaymentStatus == id);
            if (paymentStatus == null)
            {
                return NotFound();
            }

            return View("~/Views/Payments/PaymentStatus/Details.cshtml", paymentStatus);
        }

        // GET: PaymentStatus/Create
        public IActionResult Create()
        {
            return View("~/Views/Payments/PaymentStatus/Create.cshtml");
        }

        // POST: PaymentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaymentStatus,Name")] PaymentStatus paymentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Payments/PaymentStatus/Create.cshtml", paymentStatus);
        }

        // GET: PaymentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);
            if (paymentStatus == null)
            {
                return NotFound();
            }
            return View("~/Views/Payments/PaymentStatus/Edit.cshtml", paymentStatus);
        }

        // POST: PaymentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaymentStatus,Name")] PaymentStatus paymentStatus)
        {
            if (id != paymentStatus.IdPaymentStatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentStatusExists(paymentStatus.IdPaymentStatus))
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
            return View("~/Views/Payments/PaymentStatus/Edit.cshtml", paymentStatus);
        }

        // GET: PaymentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatus = await _context.PaymentStatuses
                .FirstOrDefaultAsync(m => m.IdPaymentStatus == id);
            if (paymentStatus == null)
            {
                return NotFound();
            }

            return View("~/Views/Payments/PaymentStatus/Delete.cshtml", paymentStatus);
        }

        // POST: PaymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);
            if (paymentStatus != null)
            {
                _context.PaymentStatuses.Remove(paymentStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentStatusExists(int id)
        {
            return _context.PaymentStatuses.Any(e => e.IdPaymentStatus == id);
        }
    }
}
