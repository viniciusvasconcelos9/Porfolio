using EcommerceAPI.Core.Entities;

namespace EcommerceAPI.Core.Interface
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
