using Hockeyshop.Data.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Interfaces.Products
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync(string searchTerm = null);
        Task<ProductCategory> GetByIdAsync(int id);
        Task CreateAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
