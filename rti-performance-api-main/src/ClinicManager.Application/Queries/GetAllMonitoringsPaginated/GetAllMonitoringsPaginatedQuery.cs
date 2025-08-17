using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;

namespace ClinicManager.Application.Queries.GetAllMonitoringsPaginated
{
    public class GetAllMonitoringsPaginatedQuery : IRequest<ResponseBase<PaginatedList<Monitoring>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetAllMonitoringsPaginatedQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
