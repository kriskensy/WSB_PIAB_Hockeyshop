using System.Collections.Generic;
using System.Threading.Tasks;
using Hockeyshop.Data.Data.CMS;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IContactSectionService
    {
        Task<ContactSection> GetActiveAsync();
        Task<ContactSection> GetByIdAsync(int id);
        Task<IEnumerable<ContactSection>> GetAllAsync(string searchTerm = null);
        Task CreateAsync(ContactSection contactSection);
        Task UpdateAsync(ContactSection contactSection);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
