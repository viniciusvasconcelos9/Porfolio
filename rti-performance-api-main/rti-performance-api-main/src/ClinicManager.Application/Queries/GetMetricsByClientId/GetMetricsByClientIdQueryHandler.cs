using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using Google.Apis.Calendar.v3.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetMetricsByClientId
{
    public class GetMetricsByClientIdQueryHandler : IRequestHandler<GetMetricsByClientIdQuery, ResponseBase<Metrics>>
    {
        private readonly IMetricsRepository _metricsRepository;

        public GetMetricsByClientIdQueryHandler(IMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;
        }

        public async Task<ResponseBase<Metrics>> Handle(GetMetricsByClientIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Metrics>();
            try
            {
                var metrics = await _metricsRepository.GetMetricsByClientIdAsync(request.ClientId);

                if (metrics == null)
                {
                    response.Success = false;
                    response.Message = "metrics não encontrado.";
                }
                else
                {
                    response.Success = true;
                    response.Data = metrics;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro ao obter os dados.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

    }
}
