using EcommerceAPI.Core.Entities;
using EcommerceAPI.Core.Responses;
using EcommerceAPI.Application.Commands.Create.CreateCustomerCommand;
using EcommerceAPI.Application.Commands.Update.UpdateCustomerCommand;
using EcommerceAPI.Application.Commands.Delete.DeleteCustomerCommand;
using EcommerceAPI.Application.Queries.GetAllCustomers;
using EcommerceAPI.Application.Queries.GetAllCustomersPaginated;
using EcommerceAPI.Application.Queries.GetCustomersById;
using EcommerceAPI.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar um novo cliente
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseBase<Guid>>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Atualizar cliente
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseBase<bool>>> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest("O ID da URL não corresponde ao ID do cliente enviado");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletar cliente
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Buscar cliente por Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResponseBase<Customer>>> GetCustomerById(Guid id)
        {
            var query = new GetCustomerByIdQuery(id); // ✅ passa o ID no construtor
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        /// <summary>
        /// Buscar todos os clientes
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ResponseBase<List<Customer>>>> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Buscar clientes paginados
        /// </summary>
        [HttpGet("paginated")]
        public async Task<ActionResult<ResponseBase<PaginatedList<Customer>>>> GetAllCustomersPaginated(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            var query = new GetAllCustomersPaginatedQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SearchTerm = searchTerm
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
