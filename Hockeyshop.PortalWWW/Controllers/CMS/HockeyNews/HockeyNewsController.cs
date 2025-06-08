using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.PortalWWW.Controllers.CMS.HockeyNews
{
    public class HockeyNewsController : BaseController
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

            var shopRules = await _context.ShopRules
                .Include(r => r.IconLibrary)
                .Where(r => r.DisplayOrder.HasValue)
                .OrderBy(r => r.DisplayOrder)
                .Take(3)
                .Select(r => new ShopRulesViewModel
                {
                    IdShopRule = r.IdShopRule,
                    Title = r.Title,
                    Content = r.Content,
                    IconClass = r.IconLibrary.ClassName
                })
                .ToListAsync();

            ViewBag.ShopRules = shopRules;

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

            var shopRules = await _context.ShopRules
                .Include(r => r.IconLibrary)
                .Where(r => r.DisplayOrder.HasValue)
                .OrderBy(r => r.DisplayOrder)
                .Take(3)
                .Select(r => new ShopRulesViewModel
                {
                    IdShopRule = r.IdShopRule,
                    Title = r.Title,
                    Content = r.Content,
                    IconClass = r.IconLibrary.ClassName
                })
                .ToListAsync();

            ViewBag.ShopRules = shopRules;

            return View("~/Views/CMS/HockeyNews/Details.cshtml", hockeyNews);
        }


        private bool HockeyNewsExists(int id)
        {
            return _context.HockeyNews.Any(e => e.IdHockeyNews == id);
        }
    }
}
