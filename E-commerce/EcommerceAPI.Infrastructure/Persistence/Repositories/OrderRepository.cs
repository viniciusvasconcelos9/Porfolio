using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using EcommerceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceDbContext _context;

    public OrderRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id) =>
        await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
        await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .ToListAsync();

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetAllOrdersPaginatedAsync(int page, int pageSize)
    {
        var query = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product);

        var totalCount = await query.CountAsync();
        var orders = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (orders, totalCount);
    }

    public async Task<IEnumerable<Order>> GetOrderByCustomerIdAsync(Guid customerId) =>
        await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .ToListAsync();

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetOrderByCustomerIdPaginatedAsync(Guid customerId, int page, int pageSize)
    {
        var query = _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product);

        var totalCount = await query.CountAsync();
        var orders = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (orders, totalCount);
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
