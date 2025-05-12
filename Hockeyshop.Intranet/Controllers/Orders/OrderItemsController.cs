using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Extensions;

namespace Hockeyshop.Intranet.Controllers.Orders
{
    public class OrderItemsController : Controller
    {
        private readonly HockeyshopContext _context;

        public OrderItemsController(HockeyshopContext context)
        {
            _context = context;
        }

        //obliczanie TotalAmount
        private async Task UpdateOrderTotal(int orderId)
        {
            var order = await _context.Orders.Include(item => item.OrderItems).FirstOrDefaultAsync(item => item.IdOrder == orderId);

            if (order != null)
            {
                var trackedOrder = await _context.Orders.FindAsync(order.IdOrder);
                trackedOrder.TotalAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice);

                await _context.SaveChangesAsync();
            }
        }

        // GET: OrderItems
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.OrderItems.Include(o => o.Order).Include(o => o.Product).ThenInclude(item => item.ProductCategory).AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.Product.ProductCategory.Name.Contains(searchTerm) ||
                    item.Product.Name.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Orders/OrderItems/Index.cshtml", model);
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
                await UpdateOrderTotal(orderItem.IdOrder);
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

                    await UpdateOrderTotal(orderItem.IdOrder);
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
            try
            {
                var orderItem = await _context.OrderItems.FindAsync(id);
                if (orderItem != null)
                {
                    _context.OrderItems.Remove(orderItem);
                }

                await _context.SaveChangesAsync();
                await UpdateOrderTotal(orderItem.IdOrder);
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

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.IdOrderItem == id);
        }
    }
}
