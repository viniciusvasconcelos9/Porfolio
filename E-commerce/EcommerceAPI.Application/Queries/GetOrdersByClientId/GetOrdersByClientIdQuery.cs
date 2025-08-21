using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetOrdersByClientId;

public record GetOrdersByClientIdQuery(Guid CustomerId) : IRequest<IEnumerable<Order>>;
