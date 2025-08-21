using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using EcommerceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDbContext _context;

    public CategoryRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id) =>
        await _context.Categories
                      .Include(c => c.Products)
                      .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAllCategoryAsync() =>
        await _context.Categories
                      .Include(c => c.Products)
                      .ToListAsync();

    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryByNameAsync(string name) =>
        await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
}
