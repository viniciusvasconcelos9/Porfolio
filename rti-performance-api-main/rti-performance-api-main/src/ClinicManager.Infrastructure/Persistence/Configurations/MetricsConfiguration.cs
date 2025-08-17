using Clinic_Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Configurations
{
    public class MetricsConfiguration : IEntityTypeConfiguration<Metrics>
    {
        public void Configure(EntityTypeBuilder<Metrics> builder)
        {
            builder.HasKey(m => m.Id);

            // Training Heart Rate and Percentage
            builder.Property(m => m.TrainingHeartRate).IsRequired().HasMaxLength(50).HasColumnName("TrainingHeartRate");
            builder.Property(m => m.TrainingHeartPercentage).IsRequired().HasMaxLength(50).HasColumnName("TrainingHeartPercentage");

            // Perceived Exertion
            builder.Property(m => m.PerceivedExertion).IsRequired().HasMaxLength(50).HasColumnName("PerceivedExertion");

            // Relationship with Client
            builder.Property(m => m.ClientId).IsRequired().HasColumnName("ClientId");
            builder.HasOne(m => m.Client).WithMany().HasForeignKey(m => m.ClientId);
        }
    }
}
