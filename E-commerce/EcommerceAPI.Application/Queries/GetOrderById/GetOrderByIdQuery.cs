using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Order?>;
