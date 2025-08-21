using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Core.Interface;
using MediatR;


namespace EcommerceAPI.Application.Commands.Create.CreateCustomerCommand
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseBase<Guid>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBase<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cpf = request.Cpf,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), // 🔹 gera o hash
                Phone = request.Phone,
                Address = request.Address
            };

            await _customerRepository.AddCustomerAsync(customer);

            return new ResponseBase<Guid>
            {
                Success = true,
                Message = "Cliente criado com sucesso",
                Data = customer.Id
            };
        }
    }
}
