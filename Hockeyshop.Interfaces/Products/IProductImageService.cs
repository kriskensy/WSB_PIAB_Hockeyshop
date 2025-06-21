using Hockeyshop.Data.Data.Products;
using Microsoft.AspNetCore.Http;

namespace Hockeyshop.Interfaces.Products
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImage>> GetAllAsync(int productId);
        Task<ProductImage> GetByIdAsync(int id);
        Task CreateAsync(int productId, IFormFile image);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
        bool Exists(int id);
    }
}

