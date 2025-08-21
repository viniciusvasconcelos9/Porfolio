using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) =>
        await _repository.GetCategoryByIdAsync(request.Id);
}
