using Hockeyshop.Data.Data.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IIconLibraryService
    {
        Task<IEnumerable<IconLibrary>> GetAllAsync(string searchTerm = null);
        Task<IconLibrary> GetByIdAsync(int id);
        Task CreateAsync(IconLibrary iconLibrary);
        Task UpdateAsync(IconLibrary iconLibrary);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
