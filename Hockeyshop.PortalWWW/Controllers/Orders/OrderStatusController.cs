using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.PortalWWW.Controllers.Orders
{
    public class OrderStatusController : BaseController
    {
        private readonly HockeyshopContext _context;

        public OrderStatusController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatuses.ToListAsync());
        }

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

            return View(orderStatus);
        }

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.IdOrderStatus == id);
        }
    }
}
