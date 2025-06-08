using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.PortalWWW.Controllers.Orders
{
    public class OrdersController : BaseController
    {
        private readonly HockeyshopContext _context;

        public OrdersController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Orders

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var orders = await _context.Orders
                .Where(o => o.IdUser == userId)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.Product)
                .Include(o => o.Payments)
                    .ThenInclude(o => o.PaymentStatus)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View("~/Views/Orders/Orders/Index.cshtml", orders);
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.Product)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(m => m.IdOrder == id);

            if (order == null)
            {
                return NotFound();
            }

            return View("~/Views/Orders/Orders/Details.cshtml", order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.IdOrder == id);
        }
    }
}
