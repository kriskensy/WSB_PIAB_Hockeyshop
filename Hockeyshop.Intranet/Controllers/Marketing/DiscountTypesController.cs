using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Marketing;
using Hockeyshop.Intranet.Models;

namespace Hockeyshop.Intranet.Controllers.Marketing
{
    public class DiscountTypesController : Controller
    {
        private readonly HockeyshopContext _context;

        public DiscountTypesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: DiscountTypes
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.DiscountTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(pm => pm.DiscountTypeName.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Marketing/DiscountTypes/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View("~/Views/Marketing/DiscountTypes/Index.cshtml", await _context.DiscountTypes.ToListAsync());
        //}

        // GET: DiscountTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes
                .FirstOrDefaultAsync(m => m.IdDiscountType == id);
            if (discountType == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/DiscountTypes/Details.cshtml", discountType);
        }

        // GET: DiscountTypes/Create
        public IActionResult Create()
        {
            return View("~/Views/Marketing/DiscountTypes/Create.cshtml");
        }

        // POST: DiscountTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDiscountType,DiscountTypeName")] DiscountType discountType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Marketing/DiscountTypes/Create.cshtml", discountType);
        }

        // GET: DiscountTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes.FindAsync(id);
            if (discountType == null)
            {
                return NotFound();
            }
            return View("~/Views/Marketing/DiscountTypes/Edit.cshtml", discountType);
        }

        // POST: DiscountTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDiscountType,DiscountTypeName")] DiscountType discountType)
        {
            if (id != discountType.IdDiscountType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountTypeExists(discountType.IdDiscountType))
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
            return View("~/Views/Marketing/DiscountTypes/Edit.cshtml", discountType);
        }

        // GET: DiscountTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes
                .FirstOrDefaultAsync(m => m.IdDiscountType == id);
            if (discountType == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/DiscountTypes/Delete.cshtml", discountType);
        }

        // POST: DiscountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var discountType = await _context.DiscountTypes.FindAsync(id);
                if (discountType != null)
                {
                    _context.DiscountTypes.Remove(discountType);
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

        private bool DiscountTypeExists(int id)
        {
            return _context.DiscountTypes.Any(e => e.IdDiscountType == id);
        }
    }
}
