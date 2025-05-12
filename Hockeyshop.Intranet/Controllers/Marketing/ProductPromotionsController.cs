using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Marketing;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Extensions;

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
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.ProductPromotions.Include(p => p.Product).ThenInclude(item => item.ProductCategory).Include(p => p.Promotion).AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item => item.Promotion.Name.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Marketing/ProductPromotions/Index.cshtml", model);
        }

        // GET: ProductPromotions/Details/5
        [HttpGet("ProductPromotions/Details/{idProduct}/{idPromotion}")]
        public async Task<IActionResult> Details(int? idProduct, int? idPromotion)
        {
            if (idProduct == null || idPromotion == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions
                .Include(p => p.Product)
                .Include(p => p.Promotion)
                .FirstOrDefaultAsync(m => m.IdProduct == idProduct && m.IdPromotion == idPromotion);

            if (productPromotion == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/ProductPromotions/Details.cshtml", productPromotion);
        }

        // GET: ProductPromotions/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name");
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name");
            return View("~/Views/Marketing/ProductPromotions/Create.cshtml");
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
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", productPromotion.IdProduct);
            ViewData["IdPromotion"] = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
            return View("~/Views/Marketing/ProductPromotions/Create.cshtml", productPromotion);
        }

        // GET: ProductPromotions/Edit/5
        [HttpGet("ProductPromotions/Edit/{idProduct}/{idPromotion}")]
        public async Task<IActionResult> Edit(int? idProduct, int? idPromotion)
        {
            if (idProduct == null || idPromotion == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions
                .FirstOrDefaultAsync(m => m.IdProduct == idProduct && m.IdPromotion == idPromotion);

            if (productPromotion == null)
            {
                return NotFound();
            }

            ViewBag.IdProduct = new SelectList(_context.Products, "IdProduct", "Name", productPromotion.IdProduct);
            ViewBag.IdPromotion = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);

            return View("~/Views/Marketing/ProductPromotions/Edit.cshtml", productPromotion);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productPromotion = await _context.ProductPromotions.FindAsync(id);
        //    if (productPromotion == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.IdProduct = new SelectList(_context.Products, "IdProduct", "Name", productPromotion.IdProduct);
        //    ViewBag.IdPromotion = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
        //    return View("~/Views/Marketing/ProductPromotions/Edit.cshtml", productPromotion);
        //}

        // POST: ProductPromotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProduct, int idPromotion, [Bind("IdProduct,IdPromotion")] ProductPromotion productPromotion)
        {
            if (idProduct != productPromotion.IdProduct || idPromotion != productPromotion.IdPromotion)
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
                    if (!ProductPromotionExists(productPromotion.IdProduct, productPromotion.IdPromotion))
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

            ViewBag.IdProduct = new SelectList(_context.Products, "IdProduct", "Name", productPromotion.IdProduct);
            ViewBag.IdPromotion = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);

            return View("~/Views/Marketing/ProductPromotions/Edit.cshtml", productPromotion);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdProduct,IdPromotion")] ProductPromotion productPromotion)
        //{
        //    if (id != productPromotion.IdProduct)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(productPromotion);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductPromotionExists(productPromotion.IdProduct))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.IdProduct = new SelectList(_context.Products, "IdProduct", "Name", productPromotion.IdProduct);
        //    ViewBag.IdPromotion = new SelectList(_context.Promotions, "IdPromotion", "Name", productPromotion.IdPromotion);
        //    return View("~/Views/Marketing/ProductPromotions/Edit.cshtml", productPromotion);
        //}

        // GET: ProductPromotions/Delete/5
        [HttpGet("ProductPromotions/Delete/{idProduct}/{idPromotion}")]
        public async Task<IActionResult> Delete(int? idProduct, int? idPromotion)
        {
            if (idProduct == null || idPromotion == null)
            {
                return NotFound();
            }

            var productPromotion = await _context.ProductPromotions
                .Include(p => p.Product)
                .Include(p => p.Promotion)
                .FirstOrDefaultAsync(m => m.IdProduct == idProduct && m.IdPromotion == idPromotion);

            if (productPromotion == null)
            {
                return NotFound();
            }

            return View("~/Views/Marketing/ProductPromotions/Delete.cshtml", productPromotion);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productPromotion = await _context.ProductPromotions
        //        .Include(p => p.Product)
        //        .Include(p => p.Promotion)
        //        .FirstOrDefaultAsync(m => m.IdProduct == id);
        //    if (productPromotion == null)
        //    {
        //        return NotFound();
        //    }

        //    return View("~/Views/Marketing/ProductPromotions/Delete.cshtml", productPromotion);
        //}

        // POST: ProductPromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProduct, int idPromotion)
        {
            try
            {
                var productPromotion = await _context.ProductPromotions
                    .FirstOrDefaultAsync(m => m.IdProduct == idProduct && m.IdPromotion == idPromotion);

                if (productPromotion != null)
                {
                    _context.ProductPromotions.Remove(productPromotion);
                    await _context.SaveChangesAsync();
                }

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

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        var productPromotion = await _context.ProductPromotions.FindAsync(id);
        //        if (productPromotion != null)
        //        {
        //            _context.ProductPromotions.Remove(productPromotion);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return View("Error", new ErrorViewModel
        //        {
        //            Message = "This record cannot be deleted because there are related records in other tables!"
        //        });
        //    }
        //}

        private bool ProductPromotionExists(int idProduct, int idPromotion)
        {
            return _context.ProductPromotions
                .Any(e => e.IdProduct == idProduct && e.IdPromotion == idPromotion);
        }

        //    private bool ProductPromotionExists(int id)
        //    {
        //        return _context.ProductPromotions.Any(e => e.IdProduct == id);
        //    }
        //}
    }
}
