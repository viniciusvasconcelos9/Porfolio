using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Application.Commands.Delete.DeleteMonitoringCommand
{
    public class DeleteMonitoringCommandHandler : IRequestHandler<DeleteMonitoringCommand, ResponseBase<string>>
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ILogger<DeleteMonitoringCommandHandler> _logger;

        public DeleteMonitoringCommandHandler(IMonitoringRepository monitoringRepository, ILogger<DeleteMonitoringCommandHandler> logger)
        {
            _monitoringRepository = monitoringRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<string>> Handle(DeleteMonitoringCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<string>();
            try
            {
                var monitoring = await _monitoringRepository.GetMonitoringByIdAsync(request.Id);
                if (monitoring == null)
                {
                    response.Success = false;
                    response.Message = "Monitoramento não encontrado.";
                    _logger.LogWarning($"Monitoramento com Id {request.Id} não encontrado.");
                    return response;
                }

                await _monitoringRepository.DeleteMonitoringAsync(request.Id);
                response.Success = true;
                response.Data = $"{request.Id} - Deleted";
                response.Message = "Monitoramento deletado com sucesso.";
                _logger.LogInformation($"Monitoramento com Id {request.Id} deletado com sucesso.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao deletar monitoramento: {ex.GetType().Name}";
                response.Errors.Add(ex.Message);
                _logger.LogError(ex, $"Erro ao deletar monitoramento com Id {request.Id}");
            }

            return response;
        }
    }
}
