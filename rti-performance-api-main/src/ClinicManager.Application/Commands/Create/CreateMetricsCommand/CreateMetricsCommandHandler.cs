using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicManager.Infrastructure.Persistence;


namespace ClinicManager.Application.Commands.Create.CreateMetricsCommand
{
    public class CreateMetricsCommandHandler : IRequestHandler<CreateMetricsCommand, ResponseBase<Guid>>
    {
        private readonly IMetricsRepository _metricsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMetricsCommandHandler(IMetricsRepository metricsRepository, IUnitOfWork unitOfWork)
        {
            _metricsRepository = metricsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<Guid>> Handle(CreateMetricsCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Guid>();

            try
            {
                var metrics = new Metrics
                {
                    Id = Guid.NewGuid(),
                    TrainingHeartRate = request.TrainingHeartRate,
                    TrainingHeartPercentage = request.TrainingHeartPercentage,
                    PerceivedExertion = request.PerceivedExertion,
                    ClientId = request.ClientId,
                    CreatedAt = DateTime.UtcNow
                };

                await _metricsRepository.AddMetricsAsync(metrics);
                await _unitOfWork.CommitAsync();

                response.Success = true;
                response.Data = metrics.Id;
                response.Message = "Métrica criada com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar métrica: {ex.Message}";
            }

            return response;
        }
    }
}
