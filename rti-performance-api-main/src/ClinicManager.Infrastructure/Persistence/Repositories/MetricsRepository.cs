using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MetricsRepository> _logger;

        public MetricsRepository(AppDbContext context, ILogger<MetricsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Metrics> AddMetricsAsync(Metrics metrics)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Repository - AddMetricsAsync() - {metrics.Id}");
                _context.Metrics.Add(metrics);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.UtcNow}] Metrics added successfully - {metrics.Id}");
                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Error adding metrics - {metrics.Id}");
                throw;
            }
        }

        public async Task DeleteMetricsAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Repository - DeleteMetricsAsync() - MetricsId: {id}");
                var metrics = await _context.Metrics.FindAsync(id);
                if (metrics != null)
                {
                    _context.Metrics.Remove(metrics);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[{DateTime.UtcNow}] Metrics deleted successfully - MetricsId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.UtcNow}] Metrics not found - MetricsId: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Error deleting metrics - MetricsId: {id}");
                throw;
            }
        }

        public async Task UpdateMetricsAsync(Metrics metrics)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Repository - UpdateMetricsAsync() - MetricsId: {metrics.Id}");
                var existingMetrics = await _context.Metrics.FindAsync(metrics.Id);
                if (existingMetrics == null)
                {
                    _logger.LogWarning($"[{DateTime.UtcNow}] Metrics not found - MetricsId: {metrics.Id}");
                    throw new ArgumentException($"Não foi possível encontrar a métrica com o ID {metrics.Id}");
                }

                _context.Entry(existingMetrics).CurrentValues.SetValues(metrics);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{DateTime.UtcNow}] Metrics updated successfully - MetricsId: {metrics.Id}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"[{DateTime.UtcNow}] {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Error updating metrics - MetricsId: {metrics.Id}");
                throw new Exception("Não foi possível atualizar a métrica!", ex);
            }
        }

        public async Task<Metrics> GetMetricsByClientIdAsync(Guid clientId)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Repository - GetMetricsByClientIdAsync() - ClientId: {clientId}");
                var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.ClientId == clientId);
                _logger.LogInformation($"[{DateTime.UtcNow}] Retrieved metrics successfully - ClientId: {clientId}");
                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Error retrieving metrics - ClientId: {clientId}");
                throw;
            }
        }

        public async Task<Metrics> GetMetricsByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] Repository - GetMetricsByIdAsync() - MetricsId: {id}");
                var metrics = await _context.Metrics.FirstOrDefaultAsync(x => x.Id == id);
                if (metrics != null)
                {
                    _logger.LogInformation($"[{DateTime.UtcNow}] Metrics retrieved successfully - MetricsId: {id}");
                }
                else
                {
                    _logger.LogWarning($"[{DateTime.UtcNow}] Metrics not found - MetricsId: {id}");
                }
                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{DateTime.UtcNow}] Error retrieving Metrics - MetricsId: {id}");
                throw;
            }
        }
    }
}
