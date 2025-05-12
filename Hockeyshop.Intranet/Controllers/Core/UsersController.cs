using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Core
{
    public class UsersController : Controller
    {
        private readonly HockeyshopContext _context;

        public UsersController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Users.Include(u => u.UserRole).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.LastName.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Core/Users/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var hockeyshopContext = _context.Users.Include(u => u.UserRole);
        //    return View("~/Views/Core/Users/Index.cshtml", await hockeyshopContext.ToListAsync());
        //}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Core/Users/Details.cshtml", user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "IdUserRole", "Role");
            return View("~/Views/Core/Users/Create.cshtml");
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,FirstName,LastName,Email,PasswordHash,PostCode,City,StreetAndNumber,IdUserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "IdUserRole", "Role", user.IdUserRole);
            return View("~/Views/Core/Users/Create.cshtml", user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "IdUserRole", "Role", user.IdUserRole);
            return View("~/Views/Core/Users/Edit.cshtml", user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,FirstName,LastName,Email,PasswordHash,PostCode,City,StreetAndNumber,IdUserRole")] User user)
        {
            if (id != user.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdUser))
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
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "IdUserRole", "Role", user.IdUserRole);
            return View("~/Views/Core/Users/Edit.cshtml", user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Core/Users/Delete.cshtml", user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
