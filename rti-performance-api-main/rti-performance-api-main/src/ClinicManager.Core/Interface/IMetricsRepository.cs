using Clinic_Manager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic_Manager.Core.Interface
{
    public interface IMetricsRepository
    {
        Task<Metrics> AddMetricsAsync(Metrics metrics);
        Task DeleteMetricsAsync(Guid id);
        Task UpdateMetricsAsync(Metrics metrics);
        Task<Metrics> GetMetricsByClientIdAsync(Guid clientId);
        Task<Metrics> GetMetricsByIdAsync(Guid id);
    }
}
