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
    public class ContactSectionService : BaseService, IContactSectionService
    {
        public ContactSectionService(HockeyshopContext context) : base(context) { }

        public async Task<ContactSection> GetActiveAsync() => await _context.Set<ContactSection>().FirstOrDefaultAsync(x => x.IsActive);
        public async Task<ContactSection> GetByIdAsync(int id) => await _context.Set<ContactSection>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<ContactSection>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.Set<ContactSection>().AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(x => x.Title.Contains(searchTerm) || x.Address.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.Phone.Contains(searchTerm));
            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task CreateAsync(ContactSection contactSection)
        {
            if (contactSection.IsActive) await DeactivateAllAsync();
            _context.Set<ContactSection>().Add(contactSection);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ContactSection contactSection)
        {
            if (contactSection.IsActive) await DeactivateAllExceptAsync(contactSection.Id);
            _context.Set<ContactSection>().Update(contactSection);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<ContactSection>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<ContactSection>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public bool Exists(int id) => _context.Set<ContactSection>().Any(x => x.Id == id);

        private async Task DeactivateAllAsync()
        {
            var all = await _context.Set<ContactSection>().Where(x => x.IsActive).ToListAsync();
            foreach (var item in all) item.IsActive = false;
        }
        private async Task DeactivateAllExceptAsync(int exceptId)
        {
            var all = await _context.Set<ContactSection>().Where(x => x.IsActive && x.Id != exceptId).ToListAsync();
            foreach (var item in all) item.IsActive = false;
        }
    }
}
