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

namespace ClinicManager.Application.Commands.Update.UpdateMonitoringCommand
{
    public class UpdateMonitoringCommandHandler : IRequestHandler<UpdateMonitoringCommand, ResponseBase<Monitoring>>
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ILogger<UpdateMonitoringCommandHandler> _logger;

        public UpdateMonitoringCommandHandler(IMonitoringRepository monitoringRepository, ILogger<UpdateMonitoringCommandHandler> logger)
        {
            _monitoringRepository = monitoringRepository;
            _logger = logger;
        }

        public async Task<ResponseBase<Monitoring>> Handle(UpdateMonitoringCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Monitoring>();
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

                // Atualizando apenas as propriedades fornecidas
                if (request.PreExerciseBloodPressure != null)
                    monitoring.PreExerciseBloodPressure = request.PreExerciseBloodPressure;
                if (request.PostExerciseBloodPressure != null)
                    monitoring.PostExerciseBloodPressure = request.PostExerciseBloodPressure;
                if (request.PreExerciseSpo2 != null)
                    monitoring.PreExerciseSpo2 = request.PreExerciseSpo2;
                if (request.PreExerciseHeartRate != null)
                    monitoring.PreExerciseHeartRate = request.PreExerciseHeartRate;
                if (request.DuringExerciseHeartRate != null)
                    monitoring.DuringExerciseHeartRate = request.DuringExerciseHeartRate;
                if (request.PostExerciseHeartRate != null)
                    monitoring.PostExerciseHeartRate = request.PostExerciseHeartRate;
                if (request.BloodGlucose != null)
                    monitoring.BloodGlucose = request.BloodGlucose;
                if (request.TrainingHeartRateAndPercentage != null)
                    monitoring.TrainingHeartRateAndPercentage = request.TrainingHeartRateAndPercentage;
                if (request.PerceivedExertion != null)
                    monitoring.PerceivedExertion = request.PerceivedExertion;
                if (request.Observations != null)
                    monitoring.Observations = request.Observations;
                if (request.CreatedAt != null)
                    monitoring.CreatedAt = request.CreatedAt;
                monitoring.UpdatedAt = DateTime.UtcNow;

                await _monitoringRepository.UpdateMonitoringAsync(monitoring);

                response.Success = true;
                response.Data = monitoring;
                response.Message = "Monitoramento atualizado com sucesso.";
                _logger.LogInformation($"Monitoramento com Id {request.Id} atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar monitoramento: {ex.GetType().Name}";
                response.Errors.Add(ex.Message);
                _logger.LogError(ex, $"Erro ao atualizar monitoramento com Id {request.Id}");
            }

            return response;
        }
    }
}
