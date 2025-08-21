using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteCategoryCommand;

public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;
