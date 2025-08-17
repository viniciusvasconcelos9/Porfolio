using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateLoginCommand
{
    public class CreateLoginCommand : IRequest<ResponseBase<string>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
