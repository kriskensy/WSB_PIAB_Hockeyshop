using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Marketing;

namespace Hockeyshop.Intranet.Controllers.Marketing
{
    public class PromotionsController : Controller
    {
        private readonly HockeyshopContext _context;

        public PromotionsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: Promotions
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.Promotions.Include(p => p.DiscountType).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.Name.Contains(searchTerm));
            }

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Marketing/Promotions/Index.cshtml", model);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var hockeyshopContext = _context.Promotions.Include(p => p.DiscountType);
        //    return View("~/Views/Marketing/Promotions/Index.cshtml", await hockeyshopContext.ToListAsync());
        //}

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .Include(p => p.DiscountType)
                .FirstOrDefaultAsync(m => m.IdPromotion == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/Promotions/Details.cshtml", promotion);
        }

        // GET: Promotions/Create
        public IActionResult Create()
        {
            ViewData["IdDiscountType"] = new SelectList(_context.DiscountTypes, "IdDiscountType", "DiscountTypeName");
            return View("~/Views/Marketing/Promotions/Create.cshtml");
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPromotion,Name,IdDiscountType,DiscountValue,StartDate,EndDate,Active")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDiscountType"] = new SelectList(_context.DiscountTypes, "IdDiscountType", "DiscountTypeName", promotion.IdDiscountType);
            return View("~/Views/Marketing/Promotions/Create.cshtml", promotion);
        }

        // GET: Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            ViewData["IdDiscountType"] = new SelectList(_context.DiscountTypes, "IdDiscountType", "DiscountTypeName", promotion.IdDiscountType);
            return View("~/Views/Marketing/Promotions/Edit.cshtml", promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPromotion,Name,IdDiscountType,DiscountValue,StartDate,EndDate,Active")] Promotion promotion)
        {
            if (id != promotion.IdPromotion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.IdPromotion))
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
            ViewData["IdDiscountType"] = new SelectList(_context.DiscountTypes, "IdDiscountType", "DiscountTypeName", promotion.IdDiscountType);
            return View("~/Views/Marketing/Promotions/Edit.cshtml", promotion);
        }

        // GET: Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .Include(p => p.DiscountType)
                .FirstOrDefaultAsync(m => m.IdPromotion == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/Promotions/Delete.cshtml", promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotions.Any(e => e.IdPromotion == id);
        }
    }
}
