using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdService
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Service>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(request.ServiceById);
            return service;
        }
    }
}
