using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Helpers;

namespace EcommerceAPI.Core.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
        Task<PaginatedList<Customer>> GetAllCustomersPaginatedAsync(int pageIndex, int pageSize, string? searchTerm);
    }
}