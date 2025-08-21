using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ResponseBase<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
            if (customer == null)
            {
                return new ResponseBase<Customer>
                {
                    Success = false,
                    Message = "Cliente não encontrado",
                    Data = null
                };
            }

            return new ResponseBase<Customer>
            {
                Success = true,
                Message = "Cliente encontrado",
                Data = customer
            };
        }
    }
}
