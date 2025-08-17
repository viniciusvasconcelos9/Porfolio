using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdDoctor
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Doctor>
    {
        private readonly IDoctorRepository _repository;

        public GetDoctorByIdQueryHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Doctor> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _repository.GetDoctorByIdAsync(request.DoctorId);
            return doctor;
        }
    }
}
