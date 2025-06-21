using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Services.Abstraction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Services.Products
{
    public class ProductImageService : BaseService, IProductImageService
    {
        private readonly IWebHostEnvironment _env;

        public ProductImageService(HockeyshopContext context, IWebHostEnvironment env)
            : base(context)
        {
            _env = env;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.IdProduct == productId)
                .OrderByDescending(pi => pi.IdProductImage)
                .ToListAsync();
        }

        public async Task<ProductImage> GetByIdAsync(int id)
            => await _context.ProductImages.FirstOrDefaultAsync(pi => pi.IdProductImage == id);

        public async Task CreateAsync(int productId, IFormFile image)
        {
            //walidacja pliku
            if (image == null || image.Length == 0)
                throw new ArgumentException("File not selected.");

            var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
            var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if (!allowedExt.Contains(ext))
                throw new InvalidOperationException("Unsupported file format. Allowed: JPG, PNG, GIF, WEBP.");

            //rozmiar
            if (image.Length > 5 * 1024 * 1024)
                throw new InvalidOperationException("The file is too large. Maximum size: 5MB.");

            //tworzenie katalogu
            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "products");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            //unikalna nazwa pliku
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);

            //zapis dysk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            //zapis DB
            var productImage = new ProductImage
            {
                IdProduct = productId,
                ImageUrl = $"/uploads/products/{fileName}"
            };

            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                //usuwanie dysk
                if (!string.IsNullOrEmpty(productImage.ImageUrl))
                {
                    var fileName = Path.GetFileName(productImage.ImageUrl);
                    var filePath = Path.Combine(_env.WebRootPath, "uploads", "products", fileName);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                //usuwanie DB
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.IdProduct == productId)
                .OrderByDescending(pi => pi.IdProductImage)
                .ToListAsync();
        }

        public bool Exists(int id)
            => _context.ProductImages.Any(pi => pi.IdProductImage == id);
    }
}
