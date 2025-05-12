using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Orders
{
    public class OrderStatusController : Controller
    {
        private readonly HockeyshopContext _context;

        public OrderStatusController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.OrderStatuses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pm => pm.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Orders/OrderStatus/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View("~/Views/Orders/OrderStatus/Index.cshtml", await _context.OrderStatuses.ToListAsync());
        //}

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.IdOrderStatus == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/OrderStatus/Details.cshtml", orderStatus);
        }

        // GET: OrderStatus/Create
        public IActionResult Create()
        {
            return View("~/Views/Orders/OrderStatus/Create.cshtml");
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrderStatus,Name")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Orders/OrderStatus/Create.cshtml", orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View("~/Views/Orders/OrderStatus/Edit.cshtml", orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrderStatus,Name")] OrderStatus orderStatus)
        {
            if (id != orderStatus.IdOrderStatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.IdOrderStatus))
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
            return View("~/Views/Orders/OrderStatus/Edit.cshtml", orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.IdOrderStatus == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/OrderStatus/Delete.cshtml", orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var orderStatus = await _context.OrderStatuses.FindAsync(id);
                if (orderStatus != null)
                {
                    _context.OrderStatuses.Remove(orderStatus);
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

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.IdOrderStatus == id);
        }
    }
}
