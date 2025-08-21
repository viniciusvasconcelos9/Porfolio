using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Commands.Create.CreateProductCommand
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
