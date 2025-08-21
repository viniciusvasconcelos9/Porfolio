using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateCustomerCommand
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseBase<Guid>>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<Guid>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
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

            customer.Name = request.Name;
            customer.Cpf = request.Cpf;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Address = request.Address;

            if (!string.IsNullOrEmpty(request.Password))
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _customerRepository.UpdateCustomerAsync(customer);

            return new ResponseBase<Guid>
            {
                Success = true,
                Message = "Cliente atualizado com sucesso",
                Data = customer.Id
            };
        }
    }
}
