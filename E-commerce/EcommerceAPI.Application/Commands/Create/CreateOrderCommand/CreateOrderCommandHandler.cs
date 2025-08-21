using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;
using EcommerceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Application.Commands.Create.CreateOrderCommand;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly EcommerceDbContext _context; // Para buscar os produtos

    public CreateOrderHandler(IOrderRepository orderRepository, EcommerceDbContext context)
    {
        _orderRepository = orderRepository;
        _context = context;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        if (products.Count != productIds.Count)
            throw new Exception("Um ou mais produtos não foram encontrados.");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Items = request.Items.Select(i =>
            {
                var product = products.First(p => p.Id == i.ProductId);
                return new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = i.Quantity,
                    Price = product.Price // ✅ pega o valor oficial do produto
                };
            }).ToList()
        };

        order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);

        await _orderRepository.AddOrderAsync(order);
        return order.Id;
    }
}
