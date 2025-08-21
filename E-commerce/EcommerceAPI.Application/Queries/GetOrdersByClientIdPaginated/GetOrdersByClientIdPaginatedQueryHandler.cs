using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetOrdersByClientIdPaginated;

public class GetOrdersByClientIdPaginatedHandler : IRequestHandler<GetOrdersByClientIdPaginatedQuery, (IEnumerable<Order> Orders, int TotalCount)>
{
    private readonly IOrderRepository _repository;

    public GetOrdersByClientIdPaginatedHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> Handle(GetOrdersByClientIdPaginatedQuery request, CancellationToken cancellationToken) =>
        await _repository.GetOrderByCustomerIdPaginatedAsync(request.CustomerId, request.Page, request.PageSize);
}
