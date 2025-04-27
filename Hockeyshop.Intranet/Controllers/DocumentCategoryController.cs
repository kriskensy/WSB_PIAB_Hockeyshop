using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Documents;

namespace Hockeyshop.Intranet.Controllers
{
    public class DocumentCategoryController : Controller
    {
        private readonly HockeyshopContext _context;

        public DocumentCategoryController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: DocumentCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentCategory.ToListAsync());
        }

        // GET: DocumentCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategory
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (documentCategory == null)
            {
                return NotFound();
            }

            return View(documentCategory);
        }

        // GET: DocumentCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategory,Name,Description")] DocumentCategory documentCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentCategory);
        }

        // GET: DocumentCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategory.FindAsync(id);
            if (documentCategory == null)
            {
                return NotFound();
            }
            return View(documentCategory);
        }

        // POST: DocumentCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategory,Name,Description")] DocumentCategory documentCategory)
        {
            if (id != documentCategory.IdCategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentCategoryExists(documentCategory.IdCategory))
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
            return View(documentCategory);
        }

        // GET: DocumentCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentCategory = await _context.DocumentCategory
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (documentCategory == null)
            {
                return NotFound();
            }

            return View(documentCategory);
        }

        // POST: DocumentCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentCategory = await _context.DocumentCategory.FindAsync(id);
            if (documentCategory != null)
            {
                _context.DocumentCategory.Remove(documentCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentCategoryExists(int id)
        {
            return _context.DocumentCategory.Any(e => e.IdCategory == id);
        }
    }
}
