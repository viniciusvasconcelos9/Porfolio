using System.ComponentModel.DataAnnotations;

namespace Clinic_Manager.Core.Entities
{
    public class Monitoring
    {
        public Guid Id { get; set; }
        
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

        // Relationship with Client
        public Guid ClientId { get; set; }

        public Client? Client { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
