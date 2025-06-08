using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //pobiera liczbę produktów z sesji
            int cartCount = 0;
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<UserCartViewModel>(cartJson);
                cartCount = cart?.ItemCount ?? 0;
            }
            ViewBag.CartCount = cartCount;
        }
    }

}
