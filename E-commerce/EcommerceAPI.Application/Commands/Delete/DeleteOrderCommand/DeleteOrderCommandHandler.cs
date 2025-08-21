using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteOrderCommand;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public DeleteOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrderByIdAsync(request.Id);
        if (order == null) return false;

        await _repository.DeleteOrderAsync(order);
        return true;
    }
}
