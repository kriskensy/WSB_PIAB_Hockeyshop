using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Intranet.Extensions;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class ShopRulesController : Controller
    {
        private readonly HockeyshopContext _context;

        public ShopRulesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: ShopRules
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.ShopRules.Include(s => s.IconLibrary).AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.IconLibrary.Name.Contains(searchTerm) ||
                    item.Title.Contains(searchTerm) ||
                    item.Content.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/CMS/ShopRules/Index.cshtml", model);
        }

        // GET: ShopRules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopRule = await _context.ShopRules
                .Include(s => s.IconLibrary)
                .FirstOrDefaultAsync(m => m.IdShopRule == id);
            if (shopRule == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/ShopRules/Details.cshtml", shopRule);
        }

        // GET: ShopRules/Create
        public IActionResult Create()
        {
            ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "Name");
            return View("~/Views/CMS/ShopRules/Create.cshtml");
        }

        // POST: ShopRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdShopRule,IdIcon,Title,Content")] ShopRule shopRule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shopRule);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "ClassName", shopRule.IdIcon);
        //    return View("~/Views/CMS/ShopRules/Create.cshtml", shopRule);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdShopRule,IdIcon,Title,Content,DisplayOrder")] ShopRule shopRule)
        {
            if (shopRule.DisplayOrder.HasValue)
            {
                //moga byc ustawione tylko 3 karty
                var count = _context.ShopRules.Count(r => r.DisplayOrder.HasValue);
                if (count >= 3)
                {
                    ModelState.AddModelError("DisplayOrder", "You can display a maximum of 3 Shop Rules cards.");
                }

                //nr nie moga sie powtarzac
                bool duplicate = _context.ShopRules.Any(r => r.DisplayOrder == shopRule.DisplayOrder);
                if (duplicate)
                {
                    ModelState.AddModelError("DisplayOrder", "Each card must have a unique number from 1 to 3.");
                }

                //zakres
                if (shopRule.DisplayOrder < 1 || shopRule.DisplayOrder > 3)
                {
                    ModelState.AddModelError("DisplayOrder", "The card number must be in the range of 1-3.");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(shopRule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "Name", shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Create.cshtml", shopRule);
        }

        // GET: ShopRules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopRule = await _context.ShopRules.FindAsync(id);
            if (shopRule == null)
            {
                return NotFound();
            }
            ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "Name", shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Edit.cshtml", shopRule);
        }

        // POST: ShopRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdShopRule,IdIcon,Title,Content")] ShopRule shopRule)
        //{
        //    if (id != shopRule.IdShopRule)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(shopRule);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ShopRuleExists(shopRule.IdShopRule))
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
        //    ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "ClassName", shopRule.IdIcon);
        //    return View("~/Views/CMS/ShopRules/Edit.cshtml", shopRule);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdShopRule,IdIcon,Title,Content,DisplayOrder")] ShopRule shopRule)
        {
            if (id != shopRule.IdShopRule)
            {
                return NotFound();
            }

            if (shopRule.DisplayOrder.HasValue)
            {
                //max 3 karty
                var count = _context.ShopRules.Count(r => r.DisplayOrder.HasValue && r.IdShopRule != shopRule.IdShopRule);
                if (count >= 3)
                {
                    ModelState.AddModelError("DisplayOrder", "You can display a maximum of 3 Shop Rules cards.");
                }

                //nr nie moga sie powtarzac
                bool duplicate = _context.ShopRules.Any(r => r.DisplayOrder == shopRule.DisplayOrder && r.IdShopRule != shopRule.IdShopRule);
                if (duplicate)
                {
                    ModelState.AddModelError("DisplayOrder", "Each card must have a unique number from 1 to 3.");
                }

                //zakres
                if (shopRule.DisplayOrder < 1 || shopRule.DisplayOrder > 3)
                {
                    ModelState.AddModelError("DisplayOrder", "The card number must be in the range of 1-3.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopRuleExists(shopRule.IdShopRule))
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
            ViewData["IdIcon"] = new SelectList(_context.IconLibraries, "IdIcon", "Name", shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Edit.cshtml", shopRule);
        }


        // GET: ShopRules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopRule = await _context.ShopRules
                .Include(s => s.IconLibrary)
                .FirstOrDefaultAsync(m => m.IdShopRule == id);
            if (shopRule == null)
            {
                return NotFound();
            }

            return View("~/Views/CMS/ShopRules/Delete.cshtml", shopRule);
        }

        // POST: ShopRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            { 
            var shopRule = await _context.ShopRules.FindAsync(id);
            if (shopRule != null)
            {
                _context.ShopRules.Remove(shopRule);
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

        private bool ShopRuleExists(int id)
        {
            return _context.ShopRules.Any(e => e.IdShopRule == id);
        }
    }
}
