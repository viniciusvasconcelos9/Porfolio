using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllOrders;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken) =>
        await _repository.GetAllOrdersAsync();
}
