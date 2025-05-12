using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Extensions;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class HockeyNewsController : Controller
    {
        private readonly HockeyshopContext _context;

        public HockeyNewsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: HockeyNews
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.HockeyNews.Include(h => h.Author).AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => 
                    item.Title.Contains(searchTerm) ||
                    item.Content.Contains(searchTerm) ||
                    item.Author.LastName.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/CMS/HockeyNews/Index.cshtml", model);
        }

        // GET: HockeyNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyNews = await _context.HockeyNews
                .Include(h => h.Author)
                .FirstOrDefaultAsync(m => m.IdHockeyNews == id);
            if (hockeyNews == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/HockeyNews/Details.cshtml", hockeyNews);
        }

        // GET: HockeyNews/Create
        public IActionResult Create()
        {
            ViewData["IdAuthor"] = new SelectList(_context.Users.Select(item => new {item.IdUser, Fullname = item.FirstName + " " + item.LastName}), "IdUser", "Fullname");
            return View("~/Views/CMS/HockeyNews/Create.cshtml");
        }

        // POST: HockeyNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHockeyNews,Title,Content,IdAuthor,CreatedAt,Published,ImageUrl")] HockeyNews hockeyNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAuthor"] = new SelectList(_context.Users.Select(item => new { item.IdUser, Fullname = item.FirstName + " " + item.LastName }), "IdUser", "Fullname", hockeyNews.IdAuthor);
            return View("~/Views/CMS/HockeyNews/Create.cshtml", hockeyNews);
        }

        // GET: HockeyNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyNews = await _context.HockeyNews.FindAsync(id);
            if (hockeyNews == null)
            {
                return NotFound();
            }
            ViewData["IdAuthor"] = new SelectList(_context.Users.Select(item => new { item.IdUser, Fullname = item.FirstName + " " + item.LastName }), "IdUser", "Fullname", hockeyNews.IdAuthor);
            return View("~/Views/CMS/HockeyNews/Edit.cshtml", hockeyNews);
        }

        // POST: HockeyNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHockeyNews,Title,Content,IdAuthor,CreatedAt,Published,ImageUrl")] HockeyNews hockeyNews)
        {
            if (id != hockeyNews.IdHockeyNews)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeyNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeyNewsExists(hockeyNews.IdHockeyNews))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAuthor"] = new SelectList(_context.Users.Select(item => new { item.IdUser, Fullname = item.FirstName + " " + item.LastName }), "IdUser", "Fullname", hockeyNews.IdAuthor);
            return View("~/Views/CMS/HockeyNews/Edit.cshtml", hockeyNews);
        }

        // GET: HockeyNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyNews = await _context.HockeyNews
                .Include(h => h.Author)
                .FirstOrDefaultAsync(m => m.IdHockeyNews == id);
            if (hockeyNews == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/HockeyNews/Delete.cshtml", hockeyNews);
        }

        // POST: HockeyNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var hockeyNews = await _context.HockeyNews.FindAsync(id);
                if (hockeyNews != null)
                {
                    _context.HockeyNews.Remove(hockeyNews);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }

        private bool HockeyNewsExists(int id)
        {
            return _context.HockeyNews.Any(e => e.IdHockeyNews == id);
        }
    }
}
