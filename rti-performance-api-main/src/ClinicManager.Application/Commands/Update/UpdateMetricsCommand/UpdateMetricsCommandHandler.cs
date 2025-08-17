using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Application.Commands.Create.CreateMonitoringCommand;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Update.UpdateMetricsCommand
{
    public class UpdateMetricsCommandHandler : IRequestHandler<UpdateMetricsCommand, ResponseBase<Metrics>>
    {
        private readonly IMetricsRepository _metricsRepository;
        private readonly ILogger<UpdateMetricsCommandHandler> _logger;

        public UpdateMetricsCommandHandler(IMetricsRepository metricsRepository, ILogger<UpdateMetricsCommandHandler> logger)
        {
            _metricsRepository = metricsRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<Metrics>> Handle(UpdateMetricsCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Metrics>();
            try
            {
                var metrics = await _metricsRepository.GetMetricsByIdAsync(request.Id);
                if (metrics == null)
                {
                    response.Success = false;
                    response.Message = "Metrics não encontrado.";
                    _logger.LogWarning($"Metrics com Id {request.Id} não encontrado.");
                    return response;
                }

                // Atualizando apenas as propriedades fornecidas

                if (request.TrainingHeartRate != null)
                    metrics.TrainingHeartRate = request.TrainingHeartRate;
                if (request.TrainingHeartPercentage != null)
                    metrics.TrainingHeartPercentage = request.TrainingHeartPercentage;
                if (request.PerceivedExertion != null)
                    metrics.PerceivedExertion = request.PerceivedExertion;


                await _metricsRepository.UpdateMetricsAsync(metrics);

                response.Success = true;
                response.Data = metrics;
                response.Message = "Metrics atualizado com sucesso.";
                _logger.LogInformation($"Metrics com Id {request.Id} atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar Metrics: {ex.GetType().Name}";
                response.Errors.Add(ex.Message);
                _logger.LogError(ex, $"Erro ao atualizar Metrics com Id {request.Id}");
            }

            return response;
        }
    }
}
