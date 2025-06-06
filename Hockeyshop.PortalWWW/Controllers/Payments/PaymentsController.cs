﻿using Microsoft.AspNetCore.Mvc;
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

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder");
            ViewData["IdPaymentMethod"] = new SelectList(_context.PaymentMethods, "IdPaymentMethod", "Name");
            ViewData["IdPaymentStatus"] = new SelectList(_context.PaymentStatuses, "IdPaymentStatus", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPayment,IdOrder,PaymentDate,IdPaymentMethod,Amount,IdPaymentStatus")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", payment.IdOrder);
            ViewData["IdPaymentMethod"] = new SelectList(_context.PaymentMethods, "IdPaymentMethod", "Name", payment.IdPaymentMethod);
            ViewData["IdPaymentStatus"] = new SelectList(_context.PaymentStatuses, "IdPaymentStatus", "Name", payment.IdPaymentStatus);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", payment.IdOrder);
            ViewData["IdPaymentMethod"] = new SelectList(_context.PaymentMethods, "IdPaymentMethod", "Name", payment.IdPaymentMethod);
            ViewData["IdPaymentStatus"] = new SelectList(_context.PaymentStatuses, "IdPaymentStatus", "Name", payment.IdPaymentStatus);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPayment,IdOrder,PaymentDate,IdPaymentMethod,Amount,IdPaymentStatus")] Payment payment)
        {
            if (id != payment.IdPayment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.IdPayment))
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
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", payment.IdOrder);
            ViewData["IdPaymentMethod"] = new SelectList(_context.PaymentMethods, "IdPaymentMethod", "Name", payment.IdPaymentMethod);
            ViewData["IdPaymentStatus"] = new SelectList(_context.PaymentStatuses, "IdPaymentStatus", "Name", payment.IdPaymentStatus);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.IdPayment == id);
        }
    }
}
