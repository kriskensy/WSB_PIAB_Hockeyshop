using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data.Help;
using Hockeyshop.Data.Data;

namespace Hockeyshop.Intranet.Controllers
{
    public class FaqEntryController : Controller
    {
        private readonly HockeyshopContext _context;

        public FaqEntryController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: FaqEntry
        public async Task<IActionResult> Index()
        {
            var hockeyshopIntranetContext = _context.FaqEntry.Include(f => f.FaqCategory);
            return View(await hockeyshopIntranetContext.ToListAsync());
        }

        // GET: FaqEntry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqEntry = await _context.FaqEntry
                .Include(f => f.FaqCategory)
                .FirstOrDefaultAsync(m => m.IdFaq == id);
            if (faqEntry == null)
            {
                return NotFound();
            }

            return View(faqEntry);
        }

        // GET: FaqEntry/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.FaqCategory, "IdCategory", "Description");
            return View();
        }

        // POST: FaqEntry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFaq,Question,Answer,CreatedAt,UpdatedAt,ViewCount,IdCategory")] FaqEntry faqEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faqEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.FaqCategory, "IdCategory", "Description", faqEntry.IdCategory);
            return View(faqEntry);
        }

        // GET: FaqEntry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqEntry = await _context.FaqEntry.FindAsync(id);
            if (faqEntry == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.FaqCategory, "IdCategory", "Description", faqEntry.IdCategory);
            return View(faqEntry);
        }

        // POST: FaqEntry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFaq,Question,Answer,CreatedAt,UpdatedAt,ViewCount,IdCategory")] FaqEntry faqEntry)
        {
            if (id != faqEntry.IdFaq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faqEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqEntryExists(faqEntry.IdFaq))
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
            ViewData["IdCategory"] = new SelectList(_context.FaqCategory, "IdCategory", "Description", faqEntry.IdCategory);
            return View(faqEntry);
        }

        // GET: FaqEntry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqEntry = await _context.FaqEntry
                .Include(f => f.FaqCategory)
                .FirstOrDefaultAsync(m => m.IdFaq == id);
            if (faqEntry == null)
            {
                return NotFound();
            }

            return View(faqEntry);
        }

        // POST: FaqEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faqEntry = await _context.FaqEntry.FindAsync(id);
            if (faqEntry != null)
            {
                _context.FaqEntry.Remove(faqEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqEntryExists(int id)
        {
            return _context.FaqEntry.Any(e => e.IdFaq == id);
        }
    }
}
