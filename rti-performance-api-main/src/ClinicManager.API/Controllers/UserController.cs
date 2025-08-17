using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateUserCommand;
using ClinicManager.Application.Queries.GetAllUsersPaginated;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<Guid>>> CreateUserCommand(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<ResponseBase<PaginatedList<User>>>> GetAllUsersPaginatedQuery([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetAllUsersPaginatedQuery(pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
