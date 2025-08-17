using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;

namespace ClinicManager.Application.Queries.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQuery : IRequest<ResponseBase<PaginatedList<User>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetAllUsersPaginatedQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
