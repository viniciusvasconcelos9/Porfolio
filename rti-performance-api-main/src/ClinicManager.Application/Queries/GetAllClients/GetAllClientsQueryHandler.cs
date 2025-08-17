using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Queries.GetAllUsersPaginated;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, ResponseBase<PaginatedList<Client>>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetAllClientsQueryHandler> _logger;

        public GetAllClientsQueryHandler(IClientRepository clientRepository, ILogger<GetAllClientsQueryHandler> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<PaginatedList<Client>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<PaginatedList<Client>>();

            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Handler - GetAllClientsQueryHandler initiated.");

                var clientsPaginated = await _clientRepository.GetAllClientsPaginatedAsync(
                    request.PageIndex,
                    request.PageSize,
                    request.SearchTerm,
                    request.Active // <-- Parâmetro atualizado
                );

                response.Success = true;
                response.Data = clientsPaginated;
                response.Message = "Clients retrieved successfully.";
                _logger.LogInformation($"[{DateTime.Now}] Clients retrieved successfully. PageIndex: {request.PageIndex}, PageSize: {request.PageSize}, SearchTerm: {request.SearchTerm}, Active: {request.Active}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving paginated clients. PageIndex: {request.PageIndex}, PageSize: {request.PageSize}, SearchTerm: {request.SearchTerm}, Active: {request.Active}");
                response.Success = false;
                response.Message = "An error occurred while retrieving clients.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }


    }
}

