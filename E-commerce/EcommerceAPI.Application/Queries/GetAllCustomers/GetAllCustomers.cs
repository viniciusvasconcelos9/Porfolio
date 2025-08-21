using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<ResponseBase<List<Customer>>>
    {
    }
}
