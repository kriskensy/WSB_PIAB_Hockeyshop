using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class IconLibrariesController : Controller
    {
        private readonly IIconLibraryService _iconLibraryService;

        public IconLibrariesController(IIconLibraryService iconLibraryService)
        {
            _iconLibraryService = iconLibraryService;
        }

        // GET: IconLibraries
        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _iconLibraryService.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/IconLibraries/Index.cshtml", model);
        }

        // GET: IconLibraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var iconLibrary = await _iconLibraryService.GetByIdAsync(id.Value);
            if (iconLibrary == null)
                return NotFound();

            return View("~/Views/CMS/IconLibraries/Details.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Create
        public IActionResult Create()
        {
            return View("~/Views/CMS/IconLibraries/Create.cshtml");
        }

        // POST: IconLibraries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIcon,Name,ClassName")] IconLibrary iconLibrary)
        {
            if (ModelState.IsValid)
            {
                await _iconLibraryService.CreateAsync(iconLibrary);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/IconLibraries/Create.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var iconLibrary = await _iconLibraryService.GetByIdAsync(id.Value);
            if (iconLibrary == null)
                return NotFound();

            return View("~/Views/CMS/IconLibraries/Edit.cshtml", iconLibrary);
        }

        // POST: IconLibraries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIcon,Name,ClassName")] IconLibrary iconLibrary)
        {
            if (id != iconLibrary.IdIcon)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _iconLibraryService.UpdateAsync(iconLibrary);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IconLibraryExists(iconLibrary.IdIcon))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/IconLibraries/Edit.cshtml", iconLibrary);
        }

        // GET: IconLibraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var iconLibrary = await _iconLibraryService.GetByIdAsync(id.Value);
            if (iconLibrary == null)
                return NotFound();

            return View("~/Views/CMS/IconLibraries/Delete.cshtml", iconLibrary);
        }

        // POST: IconLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _iconLibraryService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }

        private bool IconLibraryExists(int id)
        {
            return _iconLibraryService.Exists(id);
        }
    }
}
