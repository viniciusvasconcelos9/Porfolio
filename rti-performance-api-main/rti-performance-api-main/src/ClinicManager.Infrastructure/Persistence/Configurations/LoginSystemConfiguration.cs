using Clinic_Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManager.Infrastructure.Persistence.Configurations
{
    public class LoginSystemConfiguration : IEntityTypeConfiguration<LoginSystem>
    {
        public void Configure(EntityTypeBuilder<LoginSystem> builder)
        {
            builder.ToTable("LoginSystems");

            builder.Property(ls => ls.Login)
                .IsRequired();

            builder.Property(ls => ls.Password)
                .IsRequired();
        }
    }
}
