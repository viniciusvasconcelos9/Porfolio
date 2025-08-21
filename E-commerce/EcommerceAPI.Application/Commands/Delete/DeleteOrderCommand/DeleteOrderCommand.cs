using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteOrderCommand;

public record DeleteOrderCommand(Guid Id) : IRequest<bool>;
