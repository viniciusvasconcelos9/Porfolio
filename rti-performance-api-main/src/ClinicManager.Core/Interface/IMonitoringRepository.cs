using Clinic_Manager.Core.Entities;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;

namespace Clinic_Manager.Core.Interface
{
    public interface IMonitoringRepository
    {
        Task<Monitoring> AddMonitoringAsync(Monitoring monitoring);
        Task DeleteMonitoringAsync(Guid id);
        Task<List<Monitoring>> GetAllMonitoringsAsync();
        Task<Monitoring> GetMonitoringByIdAsync(Guid id);
        //Task<Monitoring> GetMonitoringByIdAndClientIdAsync(Guid monitoringId, Guid clientId);
        Task UpdateMonitoringAsync(Monitoring monitoring);
        Task<PaginatedList<Monitoring>> GetAllMonitoringsPaginatedAsync(int pageIndex, int pageSize);
        Task<PaginatedList<Monitoring>> GetAllMonitoringsByIdPaginatedAsync(Guid clientId, int pageIndex, int pageSize);
    }
}
