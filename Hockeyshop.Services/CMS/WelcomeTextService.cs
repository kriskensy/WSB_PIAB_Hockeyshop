using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hockeyshop.Services.CMS
{
    public class WelcomeTextService : BaseService, IWelcomeTextService
    {
        public WelcomeTextService(HockeyshopContext context) : base(context) { }

        public async Task<WelcomeText> GetActiveAsync()
        {
            return await _context.Set<WelcomeText>().FirstOrDefaultAsync(w => w.IsActive == true);
        }

        public async Task<WelcomeText> GetByIdAsync(int id)
        {
            return await _context.Set<WelcomeText>().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<WelcomeText>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.Set<WelcomeText>().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(w => w.Title.Contains(searchTerm) || w.Description.Contains(searchTerm));
            }

            return await query.OrderByDescending(w => w.Id).ToListAsync();
        }

        public async Task CreateAsync(WelcomeText welcomeText)
        {
            if (welcomeText.IsActive)
            {
                await DeactivateAllAsync();
            }

            _context.Set<WelcomeText>().Add(welcomeText);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WelcomeText welcomeText)
        {
            if (welcomeText.IsActive)
            {
                await DeactivateAllExceptAsync(welcomeText.Id);
            }

            _context.Set<WelcomeText>().Update(welcomeText);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var welcomeText = await _context.Set<WelcomeText>().FindAsync(id);
            if (welcomeText != null)
            {
                _context.Set<WelcomeText>().Remove(welcomeText);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Set<WelcomeText>().Any(w => w.Id == id);
        }
        private async Task DeactivateAllAsync()
        {
            var activeTexts = await _context.Set<WelcomeText>().Where(w => w.IsActive).ToListAsync();
            foreach (var text in activeTexts)
            {
                text.IsActive = false;
            }
        }

        private async Task DeactivateAllExceptAsync(int exceptId)
        {
            var activeTexts = await _context.Set<WelcomeText>().Where(w => w.IsActive && w.Id != exceptId).ToListAsync();
            foreach (var text in activeTexts)
            {
                text.IsActive = false;
            }
        }
    }
}