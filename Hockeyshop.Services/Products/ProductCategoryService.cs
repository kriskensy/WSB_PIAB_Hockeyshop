using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

public class ProductCategoryService : BaseService, IProductCategoryService
{
    public ProductCategoryService(HockeyshopContext context) : base(context) { }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync(string searchTerm = null)
    {
        var query = _context.ProductCategories.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(pc => pc.Name.Contains(searchTerm));
        return await query.OrderByDescending(pc => pc.IdProductCategory).ToListAsync();
    }

    public async Task<ProductCategory> GetByIdAsync(int id)
        => await _context.ProductCategories.FirstOrDefaultAsync(pc => pc.IdProductCategory == id);

    public async Task CreateAsync(ProductCategory category)
    {
        _context.ProductCategories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductCategory category)
    {
        _context.ProductCategories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.ProductCategories.FindAsync(id);
        if (category != null)
        {
            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public bool Exists(int id)
        => _context.ProductCategories.Any(pc => pc.IdProductCategory == id);
}

