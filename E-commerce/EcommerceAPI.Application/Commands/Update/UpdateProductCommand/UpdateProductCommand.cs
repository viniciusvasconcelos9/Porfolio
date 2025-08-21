using EcommerceAPI.Core.Entities;
using MediatR;

namespace EcommerceAPI.Application.Commands.Update.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<Product?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
