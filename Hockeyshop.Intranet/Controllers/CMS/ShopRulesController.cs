using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Intranet.Controllers.CMS
{
    public class ShopRulesController : Controller
    {
        private readonly IShopRuleService _shopRuleService;

        public ShopRulesController(IShopRuleService shopRuleService)
        {
            _shopRuleService = shopRuleService;
        }

        // GET: ShopRules
        public async Task<IActionResult> Index(string searchTerm)
        {
            var model = await _shopRuleService.GetAllAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("~/Views/CMS/ShopRules/Index.cshtml", model);
        }

        // GET: ShopRules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var shopRule = await _shopRuleService.GetByIdAsync(id.Value, includeIcon: true);
            if (shopRule == null)
                return NotFound();

            return View("~/Views/CMS/ShopRules/Details.cshtml", shopRule);
        }

        // GET: ShopRules/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdIcon"] = await _shopRuleService.GetIconLibrarySelectListAsync();
            return View("~/Views/CMS/ShopRules/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdShopRule,IdIcon,Title,Content,DisplayOrder")] ShopRule shopRule)
        {
            var (isValid, error) = await _shopRuleService.ValidateDisplayOrderAsync(shopRule);
            if (!isValid)
                ModelState.AddModelError("DisplayOrder", error);

            if (ModelState.IsValid)
            {
                await _shopRuleService.CreateAsync(shopRule);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIcon"] = await _shopRuleService.GetIconLibrarySelectListAsync(shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Create.cshtml", shopRule);
        }

        // GET: ShopRules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var shopRule = await _shopRuleService.GetByIdAsync(id.Value);
            if (shopRule == null)
                return NotFound();

            ViewData["IdIcon"] = await _shopRuleService.GetIconLibrarySelectListAsync(shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Edit.cshtml", shopRule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdShopRule,IdIcon,Title,Content,DisplayOrder")] ShopRule shopRule)
        {
            if (id != shopRule.IdShopRule)
                return NotFound();

            var (isValid, error) = await _shopRuleService.ValidateDisplayOrderAsync(shopRule, isEdit: true);
            if (!isValid)
                ModelState.AddModelError("DisplayOrder", error);

            if (ModelState.IsValid)
            {
                try
                {
                    await _shopRuleService.UpdateAsync(shopRule);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_shopRuleService.Exists(shopRule.IdShopRule))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIcon"] = await _shopRuleService.GetIconLibrarySelectListAsync(shopRule.IdIcon);
            return View("~/Views/CMS/ShopRules/Edit.cshtml", shopRule);
        }

        // GET: ShopRules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var shopRule = await _shopRuleService.GetByIdAsync(id.Value, includeIcon: true);
            if (shopRule == null)
                return NotFound();

            return View("~/Views/CMS/ShopRules/Delete.cshtml", shopRule);
        }

        // POST: ShopRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _shopRuleService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }
    }
}
