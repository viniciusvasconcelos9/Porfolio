using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateMonitoringCommand
{
    public class CreateMonitoringCommandHandler : IRequestHandler<CreateMonitoringCommand, ResponseBase<Guid>>
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMonitoringCommandHandler(IUnitOfWork unitOfWork, IMonitoringRepository monitoringRepository)
        {
            _unitOfWork = unitOfWork;
            _monitoringRepository = monitoringRepository;
        }
        public async Task<ResponseBase<Guid>> Handle(CreateMonitoringCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Guid>();
            try
            {
                var monitoring = new Monitoring()
                {
                    Id = Guid.NewGuid(),
                    BloodGlucose = request.BloodGlucose,
                    ClientId = request.ClientId,

                    CreatedAt = request.CreatedAt ?? DateTime.UtcNow,

                    DuringExerciseHeartRate = request.DuringExerciseHeartRate,
                    Observations = request.Observations,
                    PerceivedExertion = request.PerceivedExertion,
                    PostExerciseBloodPressure = request.PostExerciseBloodPressure,
                    PostExerciseHeartRate = request?.PostExerciseHeartRate,
                    PreExerciseBloodPressure = request?.PreExerciseBloodPressure,
                    PreExerciseHeartRate = request?.PreExerciseHeartRate,
                    PreExerciseSpo2 = request?.PreExerciseSpo2,
                    TrainingHeartRateAndPercentage = request?.TrainingHeartRateAndPercentage,
                };


                await _monitoringRepository.AddMonitoringAsync(monitoring);
                await _unitOfWork.CommitAsync();

                response.Success = true;
                response.Data = monitoring.Id;
                response.Message = "Monitoracao criada com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar monitoracao: {ex.GetType().Name}";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
