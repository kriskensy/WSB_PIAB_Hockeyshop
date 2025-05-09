using System.Diagnostics;
using Hockeyshop.Data.Data;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HockeyshopContext _context;

        public HomeController(ILogger<HomeController> logger, HockeyshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public async Task <IActionResult> Index()
        //{
        //    ViewBag.ModelPage =
        //        (
        //            from page in _context.Page
        //            orderby page.Position
        //            select page
        //        ).ToList();
        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SticksTechnology()
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult HockeyNews()
        {
            return View();
        }
        public IActionResult OurHighlights()
        {
            return View();
        }
        public IActionResult GiftCards()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
