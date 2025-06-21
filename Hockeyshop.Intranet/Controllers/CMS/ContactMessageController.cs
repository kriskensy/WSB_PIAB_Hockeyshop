using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageService _service;
        public ContactMessageController(IContactMessageService service) { _service = service; }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _service.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/ContactMessages/Index.cshtml", model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var message = await _service.GetByIdAsync(id.Value);
            if (message == null) return NotFound();

            await _service.MarkAsReadAsync(id.Value);//oznacza jako przeczytane po wejściu w details

            return View("~/Views/CMS/ContactMessages/Details.cshtml", message);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var message = await _service.GetByIdAsync(id.Value);
            if (message == null) return NotFound();
            return View("~/Views/CMS/ContactMessages/Delete.cshtml", message);
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

