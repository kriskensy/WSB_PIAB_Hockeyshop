using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Intranet.Data;
using Hockeyshop.Intranet.Models.Help;

namespace Hockeyshop.Intranet.Controllers
{
    public class FaqCategoryController : Controller
    {
        private readonly HockeyshopIntranetContext _context;

        public FaqCategoryController(HockeyshopIntranetContext context)
        {
            _context = context;
        }

        // GET: FaqCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.FaqCategory.ToListAsync());
        }

        // GET: FaqCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqCategory = await _context.FaqCategory
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (faqCategory == null)
            {
                return NotFound();
            }

            return View(faqCategory);
        }

        // GET: FaqCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaqCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategory,Name,Description")] FaqCategory faqCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faqCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faqCategory);
        }

        // GET: FaqCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqCategory = await _context.FaqCategory.FindAsync(id);
            if (faqCategory == null)
            {
                return NotFound();
            }
            return View(faqCategory);
        }

        // POST: FaqCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategory,Name,Description")] FaqCategory faqCategory)
        {
            if (id != faqCategory.IdCategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faqCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqCategoryExists(faqCategory.IdCategory))
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
            return View(faqCategory);
        }

        // GET: FaqCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqCategory = await _context.FaqCategory
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (faqCategory == null)
            {
                return NotFound();
            }

            return View(faqCategory);
        }

        // POST: FaqCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faqCategory = await _context.FaqCategory.FindAsync(id);
            if (faqCategory != null)
            {
                _context.FaqCategory.Remove(faqCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqCategoryExists(int id)
        {
            return _context.FaqCategory.Any(e => e.IdCategory == id);
        }
    }
}
