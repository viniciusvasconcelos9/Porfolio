using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ResponseBase<List<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            return new ResponseBase<List<Customer>>
            {
                Success = true,
                Message = "Lista de clientes carregada",
                Data = customers
            };
        }
    }
}
