using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateCustomerCommand
{
    public class UpdateCustomerCommand : IRequest<ResponseBase<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; } // opcional
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
