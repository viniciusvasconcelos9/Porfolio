using MediatR;

namespace EcommerceAPI.Application.Commands.Create.CreateCategoryCommand;

public record CreateCategoryCommand(string Name) : IRequest<Guid>;
