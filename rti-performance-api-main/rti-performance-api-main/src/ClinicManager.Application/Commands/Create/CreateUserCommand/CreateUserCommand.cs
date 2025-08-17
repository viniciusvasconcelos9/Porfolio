using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateUserCommand
{
    public class CreateUserCommand : IRequest<ResponseBase<Guid>> 
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string? AccessRule { get; set; }
    }
}
