using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllOrdersPaginated;

public class GetAllOrdersPaginatedHandler : IRequestHandler<GetAllOrdersPaginatedQuery, (IEnumerable<Order> Orders, int TotalCount)>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersPaginatedHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> Handle(GetAllOrdersPaginatedQuery request, CancellationToken cancellationToken) =>
        await _repository.GetAllOrdersPaginatedAsync(request.Page, request.PageSize);
}
