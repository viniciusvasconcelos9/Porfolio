using Clinic_Manager.Core.Entities;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdPatient
{
    public class GetPatientByIdQuery : IRequest<Patient>
    {
        public int PatientId { get; }

        public GetPatientByIdQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
