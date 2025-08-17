using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using MediatR;

namespace ClinicManager.Application.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<ResponseBase<PaginatedList<Client>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
        public bool? Active { get; set; } // <-- Nome atualizado

        public GetAllClientsQuery(int pageIndex, int pageSize, string searchTerm = null, bool? active = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            Active = active;
        }
    }

}

