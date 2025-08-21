using EcommerceAPI.Application.Commands.Create.CreateOrderCommand;
using EcommerceAPI.Application.Commands.Update.UpdateOrderCommand;
using EcommerceAPI.Application.Commands.Delete.DeleteOrderCommand;
using EcommerceAPI.Application.Queries.GetAllOrders;
using EcommerceAPI.Application.Queries.GetAllOrdersPaginated;
using EcommerceAPI.Application.Queries.GetOrderById;
using EcommerceAPI.Application.Queries.GetOrdersByClientId;
using EcommerceAPI.Application.Queries.GetOrdersByClientIdPaginated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(result);
    }

    // GET: api/orders/paginated?page=1&pageSize=10
    [HttpGet("paginated")]
    public async Task<IActionResult> GetAllPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllOrdersPaginatedQuery(page, pageSize));
        return Ok(result);
    }

    // GET: api/orders/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    // GET: api/orders/by-customer/{customerId}
    [HttpGet("by-customer/{customerId:guid}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var result = await _mediator.Send(new GetOrdersByClientIdQuery(customerId));
        return Ok(result);
    }

    // GET: api/orders/by-customer/{customerId}/paginated?page=1&pageSize=10
    [HttpGet("by-customer/{customerId:guid}/paginated")]
    public async Task<IActionResult> GetByCustomerPaginated(Guid customerId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrdersByClientIdPaginatedQuery(customerId, page, pageSize));
        return Ok(result);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    // PUT: api/orders/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    // DELETE: api/orders/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteOrderCommand(id));
        return success ? NoContent() : NotFound();
    }
}
