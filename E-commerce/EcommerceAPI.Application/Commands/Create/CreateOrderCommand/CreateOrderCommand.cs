using MediatR;

namespace EcommerceAPI.Application.Commands.Create.CreateOrderCommand;

public record CreateOrderCommand(Guid CustomerId, List<CreateOrderItemDto> Items) : IRequest<Guid>;

public record CreateOrderItemDto(Guid ProductId, int Quantity);
