using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using EcommerceAPI.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(EcommerceDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - AddCustomerAsync() - {customer.Id}");
                _context.Customers.Add(customer);

                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Customer added successfully - {customer.Id}");
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error adding customer - {customer.Id}");
                throw;
            }
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - DeleteCustomerAsync() - CustomerId: {id}");
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[{DateTime.Now}] Customer deleted successfully - CustomerId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Customer not found - CustomerId: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error deleting customer - CustomerId: {id}");
                throw;
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllCustomersAsync()");
                var customers = await _context.Customers.AsNoTracking().ToListAsync();
                _logger.LogInformation($"[{DateTime.Now}] Retrieved {customers.Count} customers successfully");
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving customers");
                throw;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetCustomerByIdAsync() - CustomerId: {id}");
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    _logger.LogInformation($"[{DateTime.Now}] Customer retrieved successfully - CustomerId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Customer not found - CustomerId: {id}");
                }
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving customer - CustomerId: {id}");
                throw;
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - UpdateCustomerAsync() - CustomerId: {customer.Id}");
                var existingCustomer = await _context.Customers.FindAsync(customer.Id);
                if (existingCustomer == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] Customer not found - CustomerId: {customer.Id}");
                    throw new ArgumentException($"Não foi possível encontrar o cliente com o ID {customer.Id}");
                }

                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Customer updated successfully - CustomerId: {customer.Id}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"[{DateTime.Now}] {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error updating customer - CustomerId: {customer.Id}");
                throw new Exception("Não foi possível atualizar o cliente!", ex);
            }
        }

        public async Task<PaginatedList<Customer>> GetAllCustomersPaginatedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllCustomersPaginatedAsync() - PageIndex: {pageIndex}, PageSize: {pageSize}, SearchTerm: {searchTerm}");

                if (pageIndex < 1) pageIndex = 1;
                searchTerm = searchTerm?.Trim();

                var query = _context.Customers.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                             c.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                             c.Cpf.Contains(searchTerm));
                }

                query = query.OrderBy(c => c.Name);

                var totalCount = await query.CountAsync();
                var customers = await query
                                      .Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

                _logger.LogInformation($"[{DateTime.Now}] Retrieved {customers.Count} customers successfully - PageIndex: {pageIndex}, PageSize: {pageSize}");

                return new PaginatedList<Customer>(customers, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving customers - PageIndex: {pageIndex}, PageSize: {pageSize}, SearchTerm: {searchTerm}");
                throw;
            }
        }
    }
}
