using Microsoft.AspNetCore.Mvc;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartIndex()
        {
            return View();
        }
    }
}
