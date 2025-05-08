using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.Intranet.Controllers.Orders
{
    public class OrderItemsController : Controller
    {
        private readonly HockeyshopContext _context;

        public OrderItemsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            return View("~/Views/Orders/OrderItems/Index.cshtml", await hockeyshopContext.ToListAsync());
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOrderItem == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/OrderItems/Details.cshtml", orderItem);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description");
            return View("~/Views/Orders/OrderItems/Create.cshtml");
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrderItem,IdOrder,IdProduct,Quantity,UnitPrice")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderItem.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", orderItem.IdProduct);
            return View("~/Views/Orders/OrderItems/Create.cshtml", orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderItem.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", orderItem.IdProduct);
            return View("~/Views/Orders/OrderItems/Edit.cshtml", orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrderItem,IdOrder,IdProduct,Quantity,UnitPrice")] OrderItem orderItem)
        {
            if (id != orderItem.IdOrderItem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.IdOrderItem))
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
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", orderItem.IdOrder);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", orderItem.IdProduct);
            return View("~/Views/Orders/OrderItems/Edit.cshtml", orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOrderItem == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/OrderItems/Delete.cshtml", orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.IdOrderItem == id);
        }
    }
}
