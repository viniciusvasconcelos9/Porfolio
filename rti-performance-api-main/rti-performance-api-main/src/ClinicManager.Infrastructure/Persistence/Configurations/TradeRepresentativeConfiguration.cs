using Clinic_Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManager.Infrastructure.Persistence.Configurations
{
    public class TradeRepresentativeConfiguration : IEntityTypeConfiguration<TradeRepresentative>
    {
        public void Configure(EntityTypeBuilder<TradeRepresentative> builder)
        {
            builder.ToTable(nameof(TradeRepresentative));

            builder.HasKey(t => t.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(200);

            builder.Property(c => c.Address)
                .HasMaxLength(300);

            builder.Property(c => c.City)
                .HasMaxLength(100);

            builder.Property(c => c.Neighborhood)
                .HasMaxLength(100);

            builder.Property(c => c.Cep)
                .HasMaxLength(20);

            builder.Property(c => c.Uf)
                .HasMaxLength(2);

            builder.Property(c => c.Cnpj)
                .HasMaxLength(20);

            builder.Property(c => c.Cpf)
                .HasMaxLength(20);

            builder.Property(c => c.Cellfone)
                .HasMaxLength(20);

            builder.Property(c => c.BankName)
                .HasMaxLength(50);

            builder.Property(c => c.Bank)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Agency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Account)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt);
        }
    }
}
