using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Services.CMS
{
    public class IconLibraryService : BaseService, IIconLibraryService
    {
        public IconLibraryService(HockeyshopContext context) : base(context) { }

        public async Task<IEnumerable<IconLibrary>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.IconLibraries.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.Name.Contains(searchTerm) ||
                    item.ClassName.Contains(searchTerm));
            }

            return await query.OrderByDescending(il => il.IdIcon).ToListAsync();
        }

        public async Task<IconLibrary> GetByIdAsync(int id)
            => await _context.IconLibraries.FirstOrDefaultAsync(il => il.IdIcon == id);

        public async Task CreateAsync(IconLibrary iconLibrary)
        {
            _context.IconLibraries.Add(iconLibrary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IconLibrary iconLibrary)
        {
            _context.IconLibraries.Update(iconLibrary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var iconLibrary = await _context.IconLibraries.FindAsync(id);
            if (iconLibrary != null)
            {
                _context.IconLibraries.Remove(iconLibrary);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
            => _context.IconLibraries.Any(il => il.IdIcon == id);
    }
}

