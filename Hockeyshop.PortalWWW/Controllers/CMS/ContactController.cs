using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var message = new ContactMessage
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                SubmittedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _contactMessageService.CreateAsync(message);

            return Json(new { success = true, message = "Thank you! Your message has been sent." });
        }
    }
}
