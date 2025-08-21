using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<Category>>;
