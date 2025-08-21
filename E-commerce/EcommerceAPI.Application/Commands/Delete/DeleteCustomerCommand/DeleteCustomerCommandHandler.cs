using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteCustomerCommand
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResponseBase<Guid>>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<Guid>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
            if (customer == null)
            {
                return new ResponseBase<Guid>
                {
                    Success = false,
                    Message = "Cliente não encontrado",
                    Data = Guid.Empty
                };
            }

            await _customerRepository.DeleteCustomerAsync(request.Id);

            return new ResponseBase<Guid>
            {
                Success = true,
                Message = "Cliente deletado com sucesso",
                Data = request.Id
            };
        }
    }
}
