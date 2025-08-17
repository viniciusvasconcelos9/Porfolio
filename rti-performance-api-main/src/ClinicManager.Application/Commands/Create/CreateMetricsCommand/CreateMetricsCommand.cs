using Clinic_Manager.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;
using System;

namespace ClinicManager.Application.Commands.Create.CreateMetricsCommand
{
    public class CreateMetricsCommand : IRequest<ResponseBase<Guid>>
    {

        // Training Heart Rate and Percentage
        public string? TrainingHeartRate { get; set; }

        public string? TrainingHeartPercentage { get; set; }

        // Perceived Exertion
        public string? PerceivedExertion { get; set; }

        [JsonIgnore]
        public Guid ClientId { get; set; }
    }
}

