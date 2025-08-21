using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<IEnumerable<Order>>;
