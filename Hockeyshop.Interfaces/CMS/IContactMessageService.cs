using Hockeyshop.Data.Data.CMS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IContactMessageService
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync(string searchTerm = null);
        Task<ContactMessage> GetByIdAsync(int id);
        Task CreateAsync(ContactMessage message);
        Task MarkAsReadAsync(int id);
        Task DeleteAsync(int id);
        Task<int> GetUnreadCountAsync(); //do wyświetlania na kopercie w intranet
    }
}
