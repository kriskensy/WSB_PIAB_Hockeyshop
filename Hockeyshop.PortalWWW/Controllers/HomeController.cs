using Hockeyshop.Data.Data;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            //pobieram 3 najnowsze newsy
            var latestNews = await _context.HockeyNews
                .OrderByDescending(n => n.CreatedAt)
                .Take(3)
                .Include(n => n.Author)
                .ToListAsync();

            //pobieram new arrivals do karuzeli
            var newArrivals = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.NewArrival)
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.NewArrivals = newArrivals;

            return View(latestNews);
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
