using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Helpers;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Queries.GetAllCustomersPaginated
{
    public class GetAllCustomersPaginatedQueryHandler : IRequestHandler<GetAllCustomersPaginatedQuery, ResponseBase<PaginatedList<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersPaginatedQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<PaginatedList<Customer>>> Handle(GetAllCustomersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllCustomersPaginatedAsync(request.PageIndex, request.PageSize, request.SearchTerm);

            return new ResponseBase<PaginatedList<Customer>>
            {
                Success = true,
                Message = "Clientes carregados com sucesso",
                Data = customers
            };
        }
    }
}
