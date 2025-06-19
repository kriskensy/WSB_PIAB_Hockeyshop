using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class FooterSectionController : Controller
    {
        private readonly IFooterSectionService _service;
        public FooterSectionController(IFooterSectionService service) { _service = service; }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _service.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/FooterSection/Index.cshtml", model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/FooterSection/Details.cshtml", contact);
        }

        public IActionResult Create() => View("~/Views/CMS/FooterSection/Create.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FooterText,FooterLogoText,IsActive")] FooterSection footerSection)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(footerSection);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/FooterSection/Create.cshtml", footerSection);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/FooterSection/Edit.cshtml", contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FooterText,FooterLogoText,IsActive")] FooterSection footerSection)
        {
            if (id != footerSection.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(footerSection);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/FooterSection/Edit.cshtml", footerSection);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/FooterSection/Delete.cshtml", contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
