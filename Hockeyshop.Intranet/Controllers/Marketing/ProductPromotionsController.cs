using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Marketing;

namespace Hockeyshop.Intranet.Controllers.Marketing
{
    public class ProductPromotionsController : Controller
    {
        private readonly HockeyshopContext _context;

        public ProductPromotionsController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: ProductPromotions
        public async Task<IActionResult> Index()
        {
            var hockeyshopContext = _context.ProductPromotions.Include(p => p.Product).Include(p => p.Promotion);
            return View(await hockeyshopContext.ToListAsync());
        }

        // GET: ProductPromotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions
                .Include(p => p.Product)
                .Include(p => p.Promotion)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (productPromotion == null)
            {
                return NotFound();
            }

            return View(productPromotion);
        }

        // GET: ProductPromotions/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description");
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name");
            return View();
        }

        // POST: ProductPromotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,IdPromotion")] ProductPromotion productPromotion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPromotion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", productPromotion.IdProduct);
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
            return View(productPromotion);
        }

        // GET: ProductPromotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions.FindAsync(id);
            if (productPromotion == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", productPromotion.IdProduct);
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
            return View(productPromotion);
        }

        // POST: ProductPromotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,IdPromotion")] ProductPromotion productPromotion)
        {
            if (id != productPromotion.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPromotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPromotionExists(productPromotion.IdProduct))
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
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Description", productPromotion.IdProduct);
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
            return View(productPromotion);
        }

        // GET: ProductPromotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions
                .Include(p => p.Product)
                .Include(p => p.Promotion)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (productPromotion == null)
            {
                return NotFound();
            }

            return View(productPromotion);
        }

        // POST: ProductPromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPromotion = await _context.ProductPromotions.FindAsync(id);
            if (productPromotion != null)
            {
                _context.ProductPromotions.Remove(productPromotion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPromotionExists(int id)
        {
            return _context.ProductPromotions.Any(e => e.IdProduct == id);
        }
    }
}
