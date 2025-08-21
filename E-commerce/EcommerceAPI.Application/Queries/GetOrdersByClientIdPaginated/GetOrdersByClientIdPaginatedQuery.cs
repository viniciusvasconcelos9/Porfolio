using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetOrdersByClientIdPaginated;

public record GetOrdersByClientIdPaginatedQuery(Guid CustomerId, int Page, int PageSize) : IRequest<(IEnumerable<Order> Orders, int TotalCount)>;
