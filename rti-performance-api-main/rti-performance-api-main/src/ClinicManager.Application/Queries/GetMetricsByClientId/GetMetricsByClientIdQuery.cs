using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using MediatR;
using System;

namespace ClinicManager.Application.Queries.GetMetricsByClientId
{
    public class GetMetricsByClientIdQuery : IRequest<ResponseBase<Metrics>>
{
    public Guid ClientId { get; }

    public GetMetricsByClientIdQuery(Guid clientId)
    {
        ClientId = clientId;
    }
}

}
