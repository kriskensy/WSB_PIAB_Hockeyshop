using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Intranet.Extensions;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class IconLibrariesController : Controller
    {
        private readonly HockeyshopContext _context;

        public IconLibrariesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: IconLibraries
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.IconLibraries.AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.Name.Contains(searchTerm) ||
                    item.ClassName.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/CMS/IconLibraries/Index.cshtml", model);
        }

        // GET: IconLibraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iconLibrary = await _context.IconLibraries
                .FirstOrDefaultAsync(m => m.IdIcon == id);
            if (iconLibrary == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/IconLibraries/Details.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Create
        public IActionResult Create()
        {
            return View("~/Views/CMS/IconLibraries/Create.cshtml");
        }

        // POST: IconLibraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIcon,Name,ClassName")] IconLibrary iconLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iconLibrary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/IconLibraries/Create.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iconLibrary = await _context.IconLibraries.FindAsync(id);
            if (iconLibrary == null)
            {
                return NotFound();
            }
            return View("~/Views/CMS/IconLibraries/Edit.cshtml", iconLibrary);
        }

        // POST: IconLibraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIcon,Name,ClassName")] IconLibrary iconLibrary)
        {
            if (id != iconLibrary.IdIcon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iconLibrary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IconLibraryExists(iconLibrary.IdIcon))
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
            return View("~/Views/CMS/IconLibraries/Edit.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iconLibrary = await _context.IconLibraries
                .FirstOrDefaultAsync(m => m.IdIcon == id);
            if (iconLibrary == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/IconLibraries/Delete.cshtml", iconLibrary);
        }

        // POST: IconLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try 
            { 
            var iconLibrary = await _context.IconLibraries.FindAsync(id);
            if (iconLibrary != null)
            {
                _context.IconLibraries.Remove(iconLibrary);
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

        private bool IconLibraryExists(int id)
        {
            return _context.IconLibraries.Any(e => e.IdIcon == id);
        }
    }
}
