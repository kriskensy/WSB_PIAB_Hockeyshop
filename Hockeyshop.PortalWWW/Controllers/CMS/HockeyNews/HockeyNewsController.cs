using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;

namespace Hockeyshop.PortalWWW.Controllers.CMS.HockeyNews
{
    public class HockeyNewsController : Controller
    {
        private readonly HockeyshopContext _context;

        public HockeyNewsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: /CMS/HockeyNews
        public async Task<IActionResult> Index()
        {
            var news = await _context.HockeyNews
                .Include(n => n.Author)
                .Where(n => n.Published)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View("~/Views/CMS/HockeyNews/Index.cshtml", news);
        }

        // GET: /CMS/HockeyNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var hockeyNews = await _context.HockeyNews
                .Include(n => n.Author)
                .FirstOrDefaultAsync(m => m.IdHockeyNews == id);

            if (hockeyNews == null)
                return NotFound();

            return View("~/Views/CMS/HockeyNews/Details.cshtml", hockeyNews);
        }

        private bool HockeyNewsExists(int id)
        {
            return _context.HockeyNews.Any(e => e.IdHockeyNews == id);
        }
    }
}
