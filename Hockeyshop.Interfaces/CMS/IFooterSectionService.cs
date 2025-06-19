using System.Collections.Generic;
using System.Threading.Tasks;
using Hockeyshop.Data.Data.CMS;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IFooterSectionService
    {
        Task<FooterSection> GetActiveAsync();
        Task<FooterSection> GetByIdAsync(int id);
        Task<IEnumerable<FooterSection>> GetAllAsync(string searchTerm = null);
        Task CreateAsync(FooterSection footerSection);
        Task UpdateAsync(FooterSection footerSection);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
