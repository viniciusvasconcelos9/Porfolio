using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
