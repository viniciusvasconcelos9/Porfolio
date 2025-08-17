using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace ClinicManager.Application.Commands.Update.UpdateMetricsCommand
{
    public class UpdateMetricsCommand : IRequest<ResponseBase<Metrics>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        // Training Heart Rate and Percentage
        public string? TrainingHeartRate { get; set; }

        public string? TrainingHeartPercentage { get; set; }

        // Perceived Exertion
        public string? PerceivedExertion { get; set; }

    }
}
