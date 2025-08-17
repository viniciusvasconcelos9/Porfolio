using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateMetricsCommand;
using ClinicManager.Application.Commands.Delete.DeleteMonitoringCommand;
using ClinicManager.Application.Commands.Update.UpdateClientCommand;
using ClinicManager.Application.Commands.Update.UpdateMetricsCommand;


using ClinicManager.Application.Queries.GetAllMonitoringsByIdPaginated;
using ClinicManager.Application.Queries.GetAllMonitoringsPaginated;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicManager.Application.Queries.GetMetricsByClientId;

namespace ClinicManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<Guid>>> CreateMetricsCommand([FromQuery] Guid clientId, CreateMetricsCommand command)
        {
            command.ClientId = clientId;
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetMetricsByClientId(Guid clientId)
        {
            try
            {
                var metrics = await _mediator.Send(new GetMetricsByClientIdQuery(clientId));
                return Ok(metrics);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno ao buscar métricas.", Details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseBase<Metrics>>> UpdateMetricsCommand([FromQuery] Guid id, [FromBody] UpdateMetricsCommand command)
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
