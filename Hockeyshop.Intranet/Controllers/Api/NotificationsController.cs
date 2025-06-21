using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Hockeyshop.Intranet.Hubs;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.Api
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IContactMessageService _contactMessageService;

        public NotificationsController(
            IHubContext<NotificationHub> hubContext,
            IContactMessageService contactMessageService)
        {
            _hubContext = hubContext;
            _contactMessageService = contactMessageService;
        }

        [HttpPost("new-message")]
        public async Task<IActionResult> NewMessage()
        {
            int unreadCount = await _contactMessageService.GetUnreadCountAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveMessageNotification", unreadCount);
            return Ok();
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            int unreadCount = await _contactMessageService.GetUnreadCountAsync();
            return Ok(unreadCount);
        }

    }
}
