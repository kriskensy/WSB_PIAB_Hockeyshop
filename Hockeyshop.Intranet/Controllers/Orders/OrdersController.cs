using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.Intranet.Controllers.Orders
{
    public class OrdersController : Controller
    {
        private readonly HockeyshopContext _context;

        public OrdersController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.Orders.Include(o => o.OrderStatus).Include(o => o.User);
            return View("~/Views/Orders/Orders/Index.cshtml", await hockeyshopContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/Orders/Details.cshtml", order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "IdOrderStatus", "Name");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City");
            return View("~/Views/Orders/Orders/Create.cshtml");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,OrderDate,IdUser,IdOrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "IdOrderStatus", "Name", order.IdOrderStatus);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", order.IdUser);
            return View("~/Views/Orders/Orders/Create.cshtml", order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "IdOrderStatus", "Name", order.IdOrderStatus);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", order.IdUser);
            return View("~/Views/Orders/Orders/Edit.cshtml", order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,OrderDate,IdUser,IdOrderStatus")] Order order)
        {
            if (id != order.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.IdOrder))
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
            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "IdOrderStatus", "Name", order.IdOrderStatus);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", order.IdUser);
            return View("~/Views/Orders/Orders/Edit.cshtml", order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/Orders/Delete.cshtml", order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.IdOrder == id);
        }
    }
}
