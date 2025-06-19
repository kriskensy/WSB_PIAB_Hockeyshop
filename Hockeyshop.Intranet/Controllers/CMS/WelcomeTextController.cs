using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class WelcomeTextController : Controller
    {
        private readonly IWelcomeTextService _service;

        public WelcomeTextController(IWelcomeTextService service)
        {
            _service = service;
        }

        // GET: WelcomeText
        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _service.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/WelcomeText/Index.cshtml", model);
        }

        // GET: WelcomeText/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var welcomeText = await _service.GetByIdAsync(id.Value);
            if (welcomeText == null)
                return NotFound();

            return View("~/Views/CMS/WelcomeText/Details.cshtml", welcomeText);
        }

        // GET: WelcomeText/Create
        public IActionResult Create()
        {
            return View("~/Views/CMS/WelcomeText/Create.cshtml");
        }

        // POST: WelcomeText/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsActive")] WelcomeText welcomeText)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(welcomeText);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/WelcomeText/Create.cshtml", welcomeText);
        }

        // GET: WelcomeText/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var welcomeText = await _service.GetByIdAsync(id.Value);
            if (welcomeText == null)
                return NotFound();

            return View("~/Views/CMS/WelcomeText/Edit.cshtml", welcomeText);
        }

        // POST: WelcomeText/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsActive")] WelcomeText welcomeText)
        {
            if (id != welcomeText.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(welcomeText);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.Exists(welcomeText.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/WelcomeText/Edit.cshtml", welcomeText);
        }

        // GET: WelcomeText/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var welcomeText = await _service.GetByIdAsync(id.Value);
            if (welcomeText == null)
                return NotFound();

            return View("~/Views/CMS/WelcomeText/Delete.cshtml", welcomeText);
        }

        // POST: WelcomeText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
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
    }
}
