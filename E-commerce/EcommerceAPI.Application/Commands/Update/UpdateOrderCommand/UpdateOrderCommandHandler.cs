using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateOrderCommand;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public UpdateOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrderByIdAsync(request.Id);
        if (order == null) return false;

        order.UpdatedAt = DateTime.UtcNow;
        order.Items.Clear();

        foreach (var item in request.Items)
        {
            order.Items.Add(new Core.Entities.OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                OrderId = order.Id
            });
        }

        order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);

        await _repository.UpdateOrderAsync(order);
        return true;
    }
}
