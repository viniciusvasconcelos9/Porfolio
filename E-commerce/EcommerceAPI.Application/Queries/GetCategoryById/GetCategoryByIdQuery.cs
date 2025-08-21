using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Category?>;
