using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdPatient
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Patient> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(request.PatientId);
            return patient;
        }
    }
}
