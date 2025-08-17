using Clinic_Manager.Core.Entities;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdDoctor
{
    public class GetDoctorByIdQuery : IRequest<Doctor>
    {
        public int DoctorId { get; }

        public GetDoctorByIdQuery(int doctorId)
        {
            DoctorId = doctorId;
        }
    }
}
