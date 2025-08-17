using Clinic_Manager.Core.Entities;

namespace Clinic_Manager.Core.Interface
{
    public interface IServiceClinicRepository
    {
        Task<IEnumerable<ServiceClinic>> GetAllServiceClinicsAsync();
        Task<ServiceClinic> GetServiceClinicByIdAsync(int id);
        Task<ServiceClinic> AddServiceClinicAsync(ServiceClinic serviceClinic);
        Task UpdateServiceClinicAsync(ServiceClinic serviceClinic);
        Task DeleteServiceClinicAsync(int id);
    }
}
