using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Commands.Delete.DeleteMonitoringCommand
{
    public class DeleteMonitoringCommand : IRequest<ResponseBase<string>>
    {
        public Guid Id { get; set; }
    }
}
