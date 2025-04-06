using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Intranet.Data;
using Hockeyshop.Intranet.Models.CMS;

namespace Hockeyshop.Intranet.Controllers
{
    public class HockeyNewsController : Controller
    {
        private readonly HockeyshopIntranetContext _context;

        public HockeyNewsController(HockeyshopIntranetContext context)
        {
            _context = context;
        }

        // GET: HockeyNews
        public async Task<IActionResult> Index()
        {
            return View(await _context.HockeyNews.ToListAsync());
        }

        // GET: HockeyNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyNews = await _context.HockeyNews
                .FirstOrDefaultAsync(m => m.IdNews == id);
            if (hockeyNews == null)
            {
                return NotFound();
            }

            return View(hockeyNews);
        }

        // GET: HockeyNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HockeyNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNews,Name,Description")] HockeyNews hockeyNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hockeyNews);
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
            return View(hockeyNews);
        }

        // POST: HockeyNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNews,Name,Description")] HockeyNews hockeyNews)
        {
            if (id != hockeyNews.IdNews)
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
                    if (!HockeyNewsExists(hockeyNews.IdNews))
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
            return View(hockeyNews);
        }

        // GET: HockeyNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyNews = await _context.HockeyNews
                .FirstOrDefaultAsync(m => m.IdNews == id);
            if (hockeyNews == null)
            {
                return NotFound();
            }

            return View(hockeyNews);
        }

        // POST: HockeyNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeyNews = await _context.HockeyNews.FindAsync(id);
            if (hockeyNews != null)
            {
                _context.HockeyNews.Remove(hockeyNews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HockeyNewsExists(int id)
        {
            return _context.HockeyNews.Any(e => e.IdNews == id);
        }
    }
}
