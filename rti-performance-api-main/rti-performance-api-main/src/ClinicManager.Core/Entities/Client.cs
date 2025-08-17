using Clinic_Manager.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Manager.Core.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Cpf")]
        public string Cpf { get; set; }

        [Column("Cellfone")]
        public string Cellfone { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("BiologicalSex")]
        public BiologicalSexEnum BiologicalSex { get; set; }

        [Column("MaritalStatus")]
        public MaritalStatusEnum MaritalStatus { get; set; }

        [Column("Birthday")]
        public DateOnly Birthday { get; set; }

        [Column("Profession")]
        public string Profession { get; set; }

        [Column("Recommendation")]
        public RecommendationEnum Recommendation { get; set; }

        [Column("HealthInsurance")]
        public string? HealthInsurance { get; set; }

        [Column("Birthplace")]
        public string Birthplace { get; set; }

        [Column("EducationLevel")]
        public EducationLevelEnum EducationLevel { get; set; }

        [Column("CaregiverName")]
        public string? CaregiverName { get; set; }

        [Column("CaregiverContact")]
        public string? CaregiverContact { get; set; }

        [Column("EmergencyContactName")]
        public string? EmergencyContactName { get; set; }

        [Column("EmergencyContactFone")]
        public string? EmergencyContactFone { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Neighborhood")]
        public string Neighborhood { get; set; }

        [Column("Cep")]
        public string Cep { get; set; }

        [Column("Uf")]
        public string Uf { get; set; }

        [Column("Classification")]
        public ClassificationEnum Classification { get; set; }

        [Column("Status")]
        public StatusEnum Status { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}