using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateLoginCommand;
using ClinicManager.Application.Commands.Create.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<string>>> CreateLoginCommand([FromBody] CreateLoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
