using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetOrdersByClientId;

public class GetOrdersByClientIdHandler : IRequestHandler<GetOrdersByClientIdQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository;

    public GetOrdersByClientIdHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Handle(GetOrdersByClientIdQuery request, CancellationToken cancellationToken) =>
        await _repository.GetOrderByCustomerIdAsync(request.CustomerId);
}
