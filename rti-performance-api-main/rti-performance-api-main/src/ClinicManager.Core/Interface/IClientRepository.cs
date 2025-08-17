using Clinic_Manager.Core.Entities;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;

namespace Clinic_Manager.Core.Interface
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid id);
        Task<Client> AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Guid id);
        public Task<PaginatedList<Client>> GetAllClientsPaginatedAsync(int pageIndex, int pageSize, string searchTerm, bool? Active = null);
    }
}