using Clinic_Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManager.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(c => c.Id);

            // Configurando propriedades obrigatórias e tamanhos máximos
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Name).HasMaxLength(100).HasColumnName("Name");
            builder.Property(c => c.Cpf).IsRequired().HasMaxLength(11).HasColumnName("Cpf");
            builder.Property(c => c.Cellfone).HasMaxLength(15).HasColumnName("Cellfone");
            builder.Property(c => c.Email).HasMaxLength(100).HasColumnName("Email");
            builder.Property(c => c.Profession).HasMaxLength(100).HasColumnName("Profession");
            builder.Property(c => c.Birthplace).HasMaxLength(100).HasColumnName("Birthplace");
            builder.Property(c => c.Address).HasMaxLength(200).HasColumnName("Address");
            builder.Property(c => c.City).HasMaxLength(100).HasColumnName("City");
            builder.Property(c => c.Neighborhood).HasMaxLength(100).HasColumnName("Neighborhood");
            builder.Property(c => c.Cep).HasMaxLength(8).HasColumnName("Cep");
            builder.Property(c => c.Uf).HasMaxLength(2).HasColumnName("Uf");

            // Configurando propriedades opcionais
            builder.Property(c => c.HealthInsurance).HasMaxLength(100).HasColumnName("HealthInsurance");
            builder.Property(c => c.CaregiverName).HasMaxLength(100).HasColumnName("CaregiverName");
            builder.Property(c => c.CaregiverContact).HasMaxLength(15).HasColumnName("CaregiverContact");
            builder.Property(c => c.EmergencyContactName).HasMaxLength(100).HasColumnName("EmergencyContactName");
            builder.Property(c => c.EmergencyContactFone).HasMaxLength(15).HasColumnName("EmergencyContactFone");

            // Configurando enumerações sem conversão
            builder.Property(c => c.BiologicalSex).HasColumnName("BiologicalSex");
            builder.Property(c => c.MaritalStatus).HasColumnName("MaritalStatus");
            builder.Property(c => c.Recommendation).HasColumnName("Recommendation");
            builder.Property(c => c.EducationLevel).HasColumnName("EducationLevel");
            builder.Property(c => c.Classification).HasColumnName("Classification");
            builder.Property(c => c.Status).HasColumnName("Status");

            // Configurando propriedades de data
            builder.Property(c => c.Birthday).HasColumnName("Birthday");
            builder.Property(c => c.CreatedAt).IsRequired().HasColumnName("CreatedAt");
            builder.Property(c => c.UpdatedAt).IsRequired(false).HasColumnName("UpdatedAt");

            // Configurando a propriedade Active
            builder.Property(c => c.Active).IsRequired().HasColumnName("Active");

            // Configurando índices únicos
            builder.HasIndex(c => c.Cpf).IsUnique();
            builder.HasIndex(c => c.Id).IsUnique();
        }
    }
}