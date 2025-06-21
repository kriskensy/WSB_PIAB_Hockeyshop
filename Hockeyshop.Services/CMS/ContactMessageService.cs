using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Interfaces.CMS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockeyshop.Services.CMS
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly HockeyshopContext _context;

        public ContactMessageService(HockeyshopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllAsync(string searchTerm = null)
        {
            var query = _context.ContactMessages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m =>
                    m.Name.Contains(searchTerm) ||
                    m.Email.Contains(searchTerm) ||
                    m.Message.Contains(searchTerm));
            }

            return await query.OrderByDescending(m => m.SubmittedAt).ToListAsync();
        }

        public async Task<ContactMessage> GetByIdAsync(int id)
            => await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);

        public async Task CreateAsync(ContactMessage message)
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}

