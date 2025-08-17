using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Queries.GetAllMonitoringsPaginated
{
    public class GetAllMonitoringsPaginatedQueryHandler : IRequestHandler<GetAllMonitoringsPaginatedQuery, ResponseBase<PaginatedList<Monitoring>>>
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ILogger<GetAllMonitoringsPaginatedQueryHandler> _logger;

        public GetAllMonitoringsPaginatedQueryHandler(IMonitoringRepository monitoringRepository, ILogger<GetAllMonitoringsPaginatedQueryHandler> logger)
        {
            _monitoringRepository = monitoringRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<PaginatedList<Monitoring>>> Handle(GetAllMonitoringsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<PaginatedList<Monitoring>>();

            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Handler - GetUsersPaginatedQueryHandler initiated.");
                var monitoringsPaginated = await _monitoringRepository.GetAllMonitoringsPaginatedAsync(request.PageIndex, request.PageSize);

                response.Success = true;
                response.Data = monitoringsPaginated;
                response.Message = "Monitorings retrieved successfully.";
                _logger.LogInformation($"[{DateTime.Now}] Monitorings retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving paginated monitorings.");
                response.Success = false;
                response.Message = "An error occurred while retrieving monitorings.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
