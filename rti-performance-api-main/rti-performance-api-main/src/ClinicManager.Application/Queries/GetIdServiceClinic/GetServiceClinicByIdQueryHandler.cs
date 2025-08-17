using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdServiceClinic
{
    public class GetServiceClinicByIdQueryHandler : IRequestHandler<GetServiceClinicByIdQuery, ServiceClinic>
    {
        private readonly IServiceClinicRepository _repository;

        public GetServiceClinicByIdQueryHandler(IServiceClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceClinic> Handle(GetServiceClinicByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceClinic = await _repository.GetServiceClinicByIdAsync(request.ServiceClinicId);
            return serviceClinic;
        }
    }
}
