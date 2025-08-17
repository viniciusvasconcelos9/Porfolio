using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using ClinicManager.Infrastructure.Persistence.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class MonitoringRepository : IMonitoringRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MonitoringRepository> _logger;

        public MonitoringRepository(AppDbContext context, ILogger<MonitoringRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Monitoring> AddMonitoringAsync(Monitoring monitoring)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - AddMonitoringAsync() - {monitoring.Id}");
                _context.Monitorings.Add(monitoring);

                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Monitoring added successfully - {monitoring.Id}");
                return monitoring;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error adding monitoring - {monitoring.Id}");
                throw;
            }
        }

        public async Task DeleteMonitoringAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - DeleteMonitoringAsync() - MonitoringId: {id}");
                var monitoring = await _context.Monitorings.FindAsync(id);
                if (monitoring != null)
                {
                    _context.Monitorings.Remove(monitoring);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[{DateTime.Now}] Monitoring deleted successfully - MonitoringId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Monitoring not found - MonitoringId: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error deleting monitoring - MonitoringId: {id}");
                throw;
            }
        }

        public async Task<List<Monitoring>> GetAllMonitoringsAsync()
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllMonitoringsAsync()");
                var monitorings = await _context.Monitorings.ToListAsync();
                _logger.LogInformation($"[{DateTime.Now}] Retrieved {monitorings.Count} monitorings successfully");
                return monitorings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving monitorings");
                throw;
            }
        }

        public async Task<Monitoring> GetMonitoringByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetMonitoringByIdAsync() - MonitoringId: {id}");
                var monitoring = await _context.Monitorings.FirstOrDefaultAsync(x => x.Id == id);
                if (monitoring != null)
                {
                    _logger.LogInformation($"[{DateTime.Now}] Monitoring retrieved successfully - MonitoringId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.Now}] Monitoring not found - MonitoringId: {id}");
                }
                return monitoring;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving monitoring - MonitoringId: {id}");
                throw;
            }
        }

        public async Task UpdateMonitoringAsync(Monitoring monitoring)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - UpdateMonitoringAsync() - MonitoringId: {monitoring.Id}");
                var existingMonitoring = await _context.Monitorings.FindAsync(monitoring.Id);
                if (existingMonitoring == null)
                {
                    _logger.LogWarning($"[{DateTime.Now}] Monitoring not found - MonitoringId: {monitoring.Id}");
                    throw new ArgumentException($"Não foi possível encontrar o monitoramento com o ID {monitoring.Id}");
                }

                _context.Entry(existingMonitoring).CurrentValues.SetValues(monitoring);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.Now}] Monitoring updated successfully - MonitoringId: {monitoring.Id}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"[{DateTime.Now}] {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error updating monitoring - MonitoringId: {monitoring.Id}");
                throw new Exception("Não foi possível atualizar o monitoramento!", ex);
            }
        }

        public async Task<PaginatedList<Monitoring>> GetAllMonitoringsPaginatedAsync(int pageIndex, int pageSize)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllMonitoringsPaginatedAsync() - PageIndex: {pageIndex}, PageSize: {pageSize}");

                var totalCount = await _context.Monitorings.CountAsync();
                var monitorings = await _context.Monitorings
                                  .OrderByDescending(m => m.CreatedAt)  // Ordenar de forma decrescente pelos mais recentes
                                  .Skip((pageIndex - 1) * pageSize)     // Paginação
                                  .Take(pageSize)                       // Quantidade por página
                                  .ToListAsync();

                _logger.LogInformation($"[{DateTime.Now}] Retrieved {monitorings.Count} monitorings successfully - PageIndex: {pageIndex}, PageSize: {pageSize}");

                return new PaginatedList<Monitoring>(monitorings, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving monitorings - PageIndex: {pageIndex}, PageSize: {pageSize}");
                throw;
            }
        }

        public async Task<PaginatedList<Monitoring>> GetAllMonitoringsByIdPaginatedAsync(Guid clientId, int pageIndex, int pageSize)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.Now}] Repository - GetAllMonitoringsPaginatedAsync() - PageIndex: {pageIndex}, PageSize: {pageSize}, ClientId: {clientId}");

                var query = _context.Monitorings.Where(m => m.ClientId == clientId);
                var totalCount = await query.CountAsync();
                var monitorings = await query
                                        .OrderByDescending(m => m.CreatedAt)
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                _logger.LogInformation($"[{DateTime.Now}] Retrieved {monitorings.Count} monitorings successfully - PageIndex: {pageIndex}, PageSize: {pageSize}, ClientId: {clientId}");

                return new PaginatedList<Monitoring>(monitorings, totalCount, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.Now}] Error retrieving monitorings - PageIndex: {pageIndex}, PageSize: {pageSize}, ClientId: {clientId}");
                throw;
            }
        }

    }
}
