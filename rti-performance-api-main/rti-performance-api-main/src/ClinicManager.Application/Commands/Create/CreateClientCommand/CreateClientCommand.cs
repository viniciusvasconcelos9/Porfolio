using Clinic_Manager.Core.Enums;
using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateClientCommand
{
    public class CreateClientCommand : IRequest<ResponseBase<Guid>>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Cellfone { get; set; }
        public string Email { get; set; }
        public BiologicalSexEnum BiologicalSex { get; set; }
        public MaritalStatusEnum MaritalStatus { get; set; }
        public DateOnly Birthday { get; set; }
        public string Profession { get; set; }
        public RecommendationEnum Recommendation { get; set; }
        public string? HealthInsurance { get; set; }
        public string Birthplace { get; set; }
        public EducationLevelEnum EducationLevel { get; set; }
        public string? CaregiverName { get; set; }
        public string? CaregiverContact { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactFone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public ClassificationEnum Classification { get; set; }
        public StatusEnum Status { get; set; }
        public bool Active { get; set; } = true;
    }
}