using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateClientCommand;
using ClinicManager.Application.Commands.Update.UpdateClientCommand;
using ClinicManager.Application.Queries.GetAllClients;
using ClinicManager.Application.Queries.GetIdClient;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<ResponseBase<Guid>>> CreateClientCommand(CreateClientCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBase<Client>>> GetClientByIdQuery(Guid id)
        {
            var response = await _mediator.Send(new GetClientByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<ResponseBase<PaginatedList<Client>>>> GetAllClientsQuery(
    [FromQuery] int pageIndex,
    [FromQuery] int pageSize,
    [FromQuery] string searchTerm = null,
    [FromQuery] bool? active = null)
        {
            var query = new GetAllClientsQuery(pageIndex, pageSize, searchTerm, active);
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBase<Client>>> UpdateClientCommand(Guid id, [FromBody] UpdateClientCommand command)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid GUID.");
            }

            command.Id = id;
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

    }
}