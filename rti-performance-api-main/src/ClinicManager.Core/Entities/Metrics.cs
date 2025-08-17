namespace Clinic_Manager.Core.Entities
{
    public class Metrics
    {
        public Guid Id { get; set; }
        

        // Training Heart Rate and Percentage
        public string? TrainingHeartRate { get; set; }
        
        public string? TrainingHeartPercentage { get; set; }

        // Perceived Exertion
        public string? PerceivedExertion { get; set; }


        // Relationship with Client
        public Guid ClientId { get; set; }

        public Client? Client { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

    }
}
