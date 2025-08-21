using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Helpers;
using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCustomersPaginated
{
    public class GetAllCustomersPaginatedQuery : IRequest<ResponseBase<PaginatedList<Customer>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
    }
}
