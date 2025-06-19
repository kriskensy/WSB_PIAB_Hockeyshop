using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockeyshop.Services.CMS
{
    public class FooterSectionService : BaseService, IFooterSectionService
    {
        public FooterSectionService(HockeyshopContext context) : base(context) { }

        public async Task<FooterSection> GetActiveAsync() => await _context.Set<FooterSection>().FirstOrDefaultAsync(x => x.IsActive);
        public async Task<FooterSection> GetByIdAsync(int id) => await _context.Set<FooterSection>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<FooterSection>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.Set<FooterSection>().AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.FooterText.Contains(searchTerm) || x.FooterLogoText.Contains(searchTerm));
            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task CreateAsync(FooterSection footerSection)
        {
            if (footerSection.IsActive) await DeactivateAllAsync();
            _context.Set<FooterSection>().Add(footerSection);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(FooterSection footerSection)
        {
            if (footerSection.IsActive) await DeactivateAllExceptAsync(footerSection.Id);
            _context.Set<FooterSection>().Update(footerSection);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<FooterSection>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<FooterSection>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public bool Exists(int id) => _context.Set<FooterSection>().Any(x => x.Id == id);

        private async Task DeactivateAllAsync()
        {
            var all = await _context.Set<FooterSection>().Where(x => x.IsActive).ToListAsync();
            foreach (var item in all) item.IsActive = false;
        }
        private async Task DeactivateAllExceptAsync(int exceptId)
        {
            var all = await _context.Set<FooterSection>().Where(x => x.IsActive && x.Id != exceptId).ToListAsync();
            foreach (var item in all) item.IsActive = false;
        }
    }
}
