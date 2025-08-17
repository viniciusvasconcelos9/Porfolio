using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace ClinicManager.Application.Commands.Update.UpdateMonitoringCommand
{
    public class UpdateMonitoringCommand : IRequest<ResponseBase<Monitoring>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        // Blood Pressure
        public string? PreExerciseBloodPressure { get; set; }
        public string? PostExerciseBloodPressure { get; set; }

        // Spo2
        public string? PreExerciseSpo2 { get; set; }

        // Heart Rate
        public string? PreExerciseHeartRate { get; set; }
        public string? DuringExerciseHeartRate { get; set; }
        public string? PostExerciseHeartRate { get; set; }

        // Blood Glucose
        public string? BloodGlucose { get; set; }

        // Training Heart Rate and Percentage
        public string? TrainingHeartRateAndPercentage { get; set; }

        // Perceived Exertion
        public string? PerceivedExertion { get; set; }

        // Observations
        public string? Observations { get; set; }
    }
}
