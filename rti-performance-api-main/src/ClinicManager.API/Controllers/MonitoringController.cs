using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateMonitoringCommand;
using ClinicManager.Application.Commands.Delete.DeleteMonitoringCommand;
using ClinicManager.Application.Commands.Update.UpdateClientCommand;
using ClinicManager.Application.Commands.Update.UpdateMonitoringCommand;
using ClinicManager.Application.Queries.GetAllClients;
using ClinicManager.Application.Queries.GetAllMonitoringsByIdPaginated;
using ClinicManager.Application.Queries.GetAllMonitoringsPaginated;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MonitoringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<Guid>>> CreateMonitoringCommand([FromQuery] Guid clientId, CreateMonitoringCommand command)
        {
            command.ClientId = clientId;
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseBase<string>>> DeleteMonitoringCommand([FromQuery] Guid id)
        {
            var response = await _mediator.Send(new DeleteMonitoringCommand() { Id = id });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseBase<Monitoring>>> UpdateMonitoringCommand([FromQuery] Guid id, [FromBody] UpdateMonitoringCommand command)
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

        [HttpGet("paginated")]
        public async Task<ActionResult<ResponseBase<PaginatedList<Monitoring>>>> GetAllMonitoringsQuery([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetAllMonitoringsPaginatedQuery(pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<ResponseBase<PaginatedList<Monitoring>>>> GetAllMonitoringsBydIdQuery([FromQuery] Guid clientId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetAllMonitoringsByIdPaginatedQuery(clientId, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
