using EcommerceAPI.Application.Commands.Create.CreateProductCommand;
using EcommerceAPI.Application.Commands.Update.UpdateProductCommand;
using EcommerceAPI.Application.Commands.Delete.DeleteProductCommand;
using EcommerceAPI.Application.Queries.GetProductById;
using EcommerceAPI.Application.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ GET all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        // ✅ GET product by Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // ✅ POST - Create product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // ✅ PUT - Update product
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id do produto não corresponde ao payload.");

            var updatedProduct = await _mediator.Send(command);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        // ✅ DELETE - Remove product
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
