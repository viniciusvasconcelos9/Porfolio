using EcommerceAPI.Core.Responses;
using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteCustomerCommand
{
    public class DeleteCustomerCommand : IRequest<ResponseBase<Guid>>
    {
        public Guid Id { get; set; }
    }
}
