using Hockeyshop.Data.Data.CMS;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IShopRuleService
    {
        Task<IEnumerable<ShopRule>> GetAllAsync(string searchTerm = null);
        Task<ShopRule> GetByIdAsync(int id, bool includeIcon = false);
        Task CreateAsync(ShopRule shopRule);
        Task UpdateAsync(ShopRule shopRule);
        Task DeleteAsync(int id);
        bool Exists(int id);

        Task<List<SelectListItem>> GetIconLibrarySelectListAsync(int? selectedId = null);

        Task<(bool IsValid, string Error)> ValidateDisplayOrderAsync(ShopRule shopRule, bool isEdit = false);
    }
}

