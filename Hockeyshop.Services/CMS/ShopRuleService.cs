using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hockeyshop.Services.CMS
{
    public class ShopRuleService : BaseService, IShopRuleService
    {
        public ShopRuleService(HockeyshopContext context) : base(context) { }

        public async Task<IEnumerable<ShopRule>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.ShopRules.Include(s => s.IconLibrary).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.IconLibrary.Name.Contains(searchTerm) ||
                    item.Title.Contains(searchTerm) ||
                    item.Content.Contains(searchTerm));
            }

            return await query.OrderByDescending(sr => sr.IdShopRule).ToListAsync();
        }

        public async Task<ShopRule> GetByIdAsync(int id, bool includeIcon = false)
        {
            if (includeIcon)
                return await _context.ShopRules.Include(s => s.IconLibrary).FirstOrDefaultAsync(s => s.IdShopRule == id);
            else
                return await _context.ShopRules.FirstOrDefaultAsync(s => s.IdShopRule == id);
        }

        public async Task CreateAsync(ShopRule shopRule)
        {
            _context.ShopRules.Add(shopRule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopRule shopRule)
        {
            _context.ShopRules.Update(shopRule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shopRule = await _context.ShopRules.FindAsync(id);
            if (shopRule != null)
            {
                _context.ShopRules.Remove(shopRule);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
            => _context.ShopRules.Any(e => e.IdShopRule == id);

        public async Task<List<SelectListItem>> GetIconLibrarySelectListAsync(int? selectedId = null)
        {
            var icons = await _context.IconLibraries.OrderBy(i => i.Name).ToListAsync();
            return icons.Select(i => new SelectListItem
            {
                Value = i.IdIcon.ToString(),
                Text = i.Name,
                Selected = (selectedId.HasValue && i.IdIcon == selectedId.Value)
            }).ToList();
        }

        public async Task<(bool IsValid, string Error)> ValidateDisplayOrderAsync(ShopRule shopRule, bool isEdit = false)
        {
            if (!shopRule.DisplayOrder.HasValue)
                return (true, null);

            int count = _context.ShopRules.Count(r => r.DisplayOrder.HasValue && (!isEdit || r.IdShopRule != shopRule.IdShopRule));
            if (count >= 3)
                return (false, "You can display a maximum of 3 Shop Rules cards.");

            bool duplicate = _context.ShopRules.Any(r => r.DisplayOrder == shopRule.DisplayOrder && (!isEdit || r.IdShopRule != shopRule.IdShopRule));
            if (duplicate)
                return (false, "Each card must have a unique number from 1 to 3.");

            if (shopRule.DisplayOrder < 1 || shopRule.DisplayOrder > 3)
                return (false, "The card number must be in the range of 1-3.");

            return (true, null);
        }
    }
}
