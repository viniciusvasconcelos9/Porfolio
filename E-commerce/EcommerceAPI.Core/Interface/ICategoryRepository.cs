using EcommerceAPI.Core.Entities;

namespace EcommerceAPI.Core.Interface;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllCategoryAsync();
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<Category?> GetCategoryByNameAsync(string name);
}
