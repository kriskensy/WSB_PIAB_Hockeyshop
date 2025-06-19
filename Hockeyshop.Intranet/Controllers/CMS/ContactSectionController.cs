using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class ContactSectionController : Controller
    {
        private readonly IContactSectionService _service;
        public ContactSectionController(IContactSectionService service) { _service = service; }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _service.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/ContactSection/Index.cshtml", model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/ContactSection/Details.cshtml", contact);
        }

        public IActionResult Create() => View("~/Views/CMS/ContactSection/Create.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Address,Email,Phone,IsActive")] ContactSection contactSection)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(contactSection);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/ContactSection/Create.cshtml", contactSection);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/ContactSection/Edit.cshtml", contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Address,Email,Phone,IsActive")] ContactSection contactSection)
        {
            if (id != contactSection.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(contactSection);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/CMS/ContactSection/Edit.cshtml", contactSection);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _service.GetByIdAsync(id.Value);
            if (contact == null) return NotFound();
            return View("~/Views/CMS/ContactSection/Delete.cshtml", contact);
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
