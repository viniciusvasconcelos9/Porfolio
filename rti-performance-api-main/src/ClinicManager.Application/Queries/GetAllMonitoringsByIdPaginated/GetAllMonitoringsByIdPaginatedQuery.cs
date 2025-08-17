using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;

namespace ClinicManager.Application.Queries.GetAllMonitoringsByIdPaginated
{
    public class GetAllMonitoringsByIdPaginatedQuery : IRequest<ResponseBase<PaginatedList<Monitoring>>>
    {
        public Guid ClientId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetAllMonitoringsByIdPaginatedQuery(Guid clientId, int pageIndex, int pageSize)
        {
            ClientId = clientId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
