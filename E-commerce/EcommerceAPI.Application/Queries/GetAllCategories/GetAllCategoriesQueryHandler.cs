using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetAllCategoriesHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken) =>
        await _repository.GetAllCategoryAsync();
}
