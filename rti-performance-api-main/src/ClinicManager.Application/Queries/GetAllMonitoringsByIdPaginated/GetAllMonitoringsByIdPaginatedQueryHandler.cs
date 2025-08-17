using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Queries.GetAllMonitoringsByIdPaginated
{
    public class GetAllMonitoringsByIdPaginatedQueryHandler : IRequestHandler<GetAllMonitoringsByIdPaginatedQuery, ResponseBase<PaginatedList<Monitoring>>>
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ILogger<GetAllMonitoringsByIdPaginatedQueryHandler> _logger;

        public GetAllMonitoringsByIdPaginatedQueryHandler(IMonitoringRepository monitoringRepository, ILogger<GetAllMonitoringsByIdPaginatedQueryHandler> logger)
        {
            _monitoringRepository = monitoringRepository;
            _logger = logger;
        }
        public async Task<ResponseBase<PaginatedList<Monitoring>>> Handle(GetAllMonitoringsByIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<PaginatedList<Monitoring>>();

            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Handler - GetAllMonitoringsByIdPaginatedQueryHandler initiated.");
                var monitoringsByIdPaginated = await _monitoringRepository.GetAllMonitoringsByIdPaginatedAsync(request.ClientId, request.PageIndex, request.PageSize);

                response.Success = true;
                response.Data = monitoringsByIdPaginated;
                response.Message = $"Monitorings retrieved successfully. ClientId - {request.ClientId}";
                _logger.LogInformation($"[{DateTime.Now}] Monitorings by id retrieved successfully.");
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
