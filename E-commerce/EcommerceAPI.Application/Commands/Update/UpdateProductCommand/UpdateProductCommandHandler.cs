using EcommerceAPI.Application.Commands.Update.UpdateProductCommand;
using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Interface;
using MediatR;

namespace EcommerceAPI.Application.Handlers.Products
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product?>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);
            if (product == null)
                return null;

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateProductAsync(product);
            return product;
        }
    }
}
