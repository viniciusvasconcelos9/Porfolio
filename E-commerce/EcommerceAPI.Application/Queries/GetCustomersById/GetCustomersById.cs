using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQuery : IRequest<ResponseBase<Customer>>
    {
        public Guid Id { get; set; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
