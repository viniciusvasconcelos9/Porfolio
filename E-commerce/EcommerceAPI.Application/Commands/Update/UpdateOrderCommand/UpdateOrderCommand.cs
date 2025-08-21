using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateOrderCommand;

public record UpdateOrderCommand(Guid Id, List<UpdateOrderItemDto> Items) : IRequest<bool>;

public record UpdateOrderItemDto(Guid ProductId, int Quantity, decimal Price);
