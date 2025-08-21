using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
