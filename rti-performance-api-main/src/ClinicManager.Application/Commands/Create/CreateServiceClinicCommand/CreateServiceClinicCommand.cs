using Clinic_Manager.Core.Enums;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateServiceClinicCommand
{
    public class CreateServiceClinicCommand : IRequest<Unit>
    {
        public int IdPatient { get; set; }
        public int IdService { get; set; }
        public int IdDoctor { get; set; }
        public string HealthInsurance { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TypeServiceEnum TypeServices { get; set; }
    }
}
