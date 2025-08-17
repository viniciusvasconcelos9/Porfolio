using Clinic_Manager.Core.Entities;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdServiceClinic
{
    public class GetServiceClinicByIdQuery : IRequest<ServiceClinic>
    {
        public int ServiceClinicId { get; }
        public GetServiceClinicByIdQuery(int serviceClinicId)
        {
            ServiceClinicId = serviceClinicId;
        }
    }
}
