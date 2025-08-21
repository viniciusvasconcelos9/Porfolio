using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Commands.Create.CreateCustomerCommand
{
    public class CreateCustomerCommand : IRequest<ResponseBase<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // 🔹 O hash será gerado no handler
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
