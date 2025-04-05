using Microsoft.AspNetCore.Mvc;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccountIndex()
        {
            return View();
        }
    }
}
