using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllOrdersPaginated;

public record GetAllOrdersPaginatedQuery(int Page, int PageSize) : IRequest<(IEnumerable<Order> Orders, int TotalCount)>;
