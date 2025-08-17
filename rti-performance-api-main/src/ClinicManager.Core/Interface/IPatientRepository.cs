using Clinic_Manager.Core.Entities;

namespace Clinic_Manager.Core.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> GetPatientByCpfAsync(string cpf);
        Task<Patient> GetPatientByPhoneNumberAsync(string phoneNumber);
        Task<Patient> AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
    }
}
