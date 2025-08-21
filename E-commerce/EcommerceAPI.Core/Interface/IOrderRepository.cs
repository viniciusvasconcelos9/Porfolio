using EcommerceAPI.Core.Entities;

namespace EcommerceAPI.Core.Interface;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<(IEnumerable<Order> Orders, int TotalCount)> GetAllOrdersPaginatedAsync(int page, int pageSize);
    Task<IEnumerable<Order>> GetOrderByCustomerIdAsync(Guid customerId);
    Task<(IEnumerable<Order> Orders, int TotalCount)> GetOrderByCustomerIdPaginatedAsync(Guid customerId, int page, int pageSize);

    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
}
