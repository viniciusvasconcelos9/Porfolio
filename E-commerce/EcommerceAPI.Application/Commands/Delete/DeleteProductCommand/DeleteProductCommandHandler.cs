using EcommerceAPI.Application.Commands.Delete.DeleteProductCommand;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Commands.Delete.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);
            if (product == null)
                return false;

            await _repository.DeleteProductAsync(product.Id);
            return true;
        }
    }
}
