using System.Threading.Tasks;
using Hockeyshop.Data.Data.CMS;

namespace Hockeyshop.Interfaces.CMS
{
    public interface IWelcomeTextService
    {
        Task<WelcomeText> GetActiveAsync(); //pobiera isActive = true
        Task<WelcomeText> GetByIdAsync(int id);
        Task<IEnumerable<WelcomeText>> GetAllAsync(string searchTerm = null);
        Task CreateAsync(WelcomeText welcomeText);
        Task UpdateAsync(WelcomeText welcomeText);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
