using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Cart;

namespace Hockeyshop.Intranet.Controllers.Cart
{
    public class UserCartsController : Controller
    {
        private readonly HockeyshopContext _context;

        public UserCartsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: UserCarts
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.UserCarts.Include(u => u.User);
            return View("~/Views/Cart/UserCarts/Index.cshtml", await hockeyshopContext.ToListAsync());
        }

        // GET: UserCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCart = await _context.UserCarts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.IdUserCart == id);
            if (userCart == null)
            {
                return NotFound();
            }

            return View("~/Views/Cart/UserCarts/Details.cshtml", userCart);
        }

        // GET: UserCarts/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City");
            return View("~/Views/Cart/UserCarts/Create.cshtml");
        }

        // POST: UserCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUserCart,IdUser,CreatedAt")] UserCart userCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", userCart.IdUser);
            return View("~/Views/Cart/UserCarts/Create.cshtml", userCart);
        }

        // GET: UserCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCart = await _context.UserCarts.FindAsync(id);
            if (userCart == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", userCart.IdUser);
            return View("~/Views/Cart/UserCarts/Edit.cshtml", userCart);
        }

        // POST: UserCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUserCart,IdUser,CreatedAt")] UserCart userCart)
        {
            if (id != userCart.IdUserCart)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCartExists(userCart.IdUserCart))
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
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "City", userCart.IdUser);
            return View("~/Views/Cart/UserCarts/Edit.cshtml", userCart);
        }

        // GET: UserCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCart = await _context.UserCarts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.IdUserCart == id);
            if (userCart == null)
            {
                return NotFound();
            }

            return View("~/Views/Cart/UserCarts/Delete.cshtml", userCart);
        }

        // POST: UserCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCart = await _context.UserCarts.FindAsync(id);
            if (userCart != null)
            {
                _context.UserCarts.Remove(userCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCartExists(int id)
        {
            return _context.UserCarts.Any(e => e.IdUserCart == id);
        }
    }
}
