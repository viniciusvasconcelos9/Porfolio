using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateCategoryCommand;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<bool>;
