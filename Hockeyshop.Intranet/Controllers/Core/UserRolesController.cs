using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Core
{
    public class UserRolesController : Controller
    {
        private readonly HockeyshopContext _context;

        public UserRolesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.UserRoles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pm => pm.Role.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Core/UserRoles/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View("~/Views/Core/UserRoles/Index.cshtml", await _context.UserRoles.ToListAsync());
        //}

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.IdUserRole == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View("~/Views/Core/UserRoles/Details.cshtml", userRole);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            return View("~/Views/Core/UserRoles/Create.cshtml");
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUserRole,Role")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Core/UserRoles/Create.cshtml", userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return View("~/Views/Core/UserRoles/Edit.cshtml", userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUserRole,Role")] UserRole userRole)
        {
            if (id != userRole.IdUserRole)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.IdUserRole))
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
            return View("~/Views/Core/UserRoles/Edit.cshtml", userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.IdUserRole == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View("~/Views/Core/UserRoles/Delete.cshtml", userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var userRole = await _context.UserRoles.FindAsync(id);
                if (userRole != null)
                {
                    _context.UserRoles.Remove(userRole);
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

        private bool UserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.IdUserRole == id);
        }
    }
}
