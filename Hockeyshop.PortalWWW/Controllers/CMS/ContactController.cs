using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;
        private readonly IHttpClientFactory _httpClientFactory; //SignalR

        public ContactController(IContactMessageService contactMessageService,
            IHttpClientFactory httpClientFactory)
        {
            _contactMessageService = contactMessageService;
            _httpClientFactory = httpClientFactory; 
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

            var client = _httpClientFactory.CreateClient();//wywołanie api intranetu

            var intranetApiUrl = "https://localhost:7232/api/notifications/new-message";
            try
            {
                await client.PostAsync(intranetApiUrl, null);
            }
            catch (Exception ex)
            {
                //TODO logowanie dodać
            }

            return Json(new { success = true, message = "Thank you! Your message has been sent." });
        }
    }
}
