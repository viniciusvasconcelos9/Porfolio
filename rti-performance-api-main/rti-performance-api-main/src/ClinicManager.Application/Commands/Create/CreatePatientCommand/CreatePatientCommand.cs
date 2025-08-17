using MediatR;

namespace ClinicManager.Application.Commands.Create.CreatePatientCommand
{
    public class CreatePatientCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Address { get; set; }
    }
}
