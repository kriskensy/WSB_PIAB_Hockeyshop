using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Cart;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Cart
{
    public class CartItemsController : Controller
    {
        private readonly HockeyshopContext _context;

        public CartItemsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.CartItems.Include(c => c.Product).Include(c => c.UserCart).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.Product.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Cart/CartItems/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var hockeyshopContext = _context.CartItems.Include(c => c.Product).Include(c => c.UserCart);
        //    return View("~/Views/Cart/CartItems/Index.cshtml", await hockeyshopContext.ToListAsync());
        //}

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.UserCart)
                .FirstOrDefaultAsync(m => m.IdCartItem == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View("~/Views/Cart/CartItems/Details.cshtml", cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description");
            ViewData["IdUserCart"] = new SelectList(_context.UserCarts, "IdUserCart", "IdUserCart");
            return View("~/Views/Cart/CartItems/Create.cshtml");
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCartItem,IdUserCart,IdProduct,Quantity,UnitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", cartItem.IdProduct);
            ViewData["IdUserCart"] = new SelectList(_context.UserCarts, "IdUserCart", "IdUserCart", cartItem.IdUserCart);
            return View("~/Views/Cart/CartItems/Create.cshtml", cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", cartItem.IdProduct);
            ViewData["IdUserCart"] = new SelectList(_context.UserCarts, "IdUserCart", "IdUserCart", cartItem.IdUserCart);
            return View("~/Views/Cart/CartItems/Edit.cshtml", cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCartItem,IdUserCart,IdProduct,Quantity,UnitPrice")] CartItem cartItem)
        {
            if (id != cartItem.IdCartItem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.IdCartItem))
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
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", cartItem.IdProduct);
            ViewData["IdUserCart"] = new SelectList(_context.UserCarts, "IdUserCart", "IdUserCart", cartItem.IdUserCart);
            return View("~/Views/Cart/CartItems/Edit.cshtml", cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.UserCart)
                .FirstOrDefaultAsync(m => m.IdCartItem == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View("~/Views/Cart/CartItems/Delete.cshtml", cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
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

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.IdCartItem == id);
        }
    }
}
