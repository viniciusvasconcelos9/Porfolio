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
    public class MonitoringConfiguration : IEntityTypeConfiguration<Monitoring>
    {
        public void Configure(EntityTypeBuilder<Monitoring> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(c => c.CreatedAt).IsRequired().HasColumnName("CreatedAt");
            builder.Property(c => c.UpdatedAt).IsRequired(false).HasColumnName("UpdatedAt");

            // Blood Pressure
            builder.Property(m => m.PreExerciseBloodPressure).IsRequired().HasMaxLength(50).HasColumnName("PreExerciseBloodPressure");
            builder.Property(m => m.PostExerciseBloodPressure).IsRequired().HasMaxLength(50).HasColumnName("PostExerciseBloodPressure");

            // Spo2
            builder.Property(m => m.PreExerciseSpo2).IsRequired().HasMaxLength(50).HasColumnName("PreExerciseSpo2");

            // Heart Rate
            builder.Property(m => m.PreExerciseHeartRate).IsRequired().HasMaxLength(50).HasColumnName("PreExerciseHeartRate");
            builder.Property(m => m.DuringExerciseHeartRate).IsRequired().HasMaxLength(50).HasColumnName("DuringExerciseHeartRate");
            builder.Property(m => m.PostExerciseHeartRate).IsRequired().HasMaxLength(50).HasColumnName("PostExerciseHeartRate");

            // Blood Glucose
            builder.Property(m => m.BloodGlucose).IsRequired().HasMaxLength(50).HasColumnName("BloodGlucose");

            // Training Heart Rate and Percentage
            builder.Property(m => m.TrainingHeartRateAndPercentage).IsRequired().HasMaxLength(50).HasColumnName("TrainingHeartRateAndPercentage");

            // Perceived Exertion
            builder.Property(m => m.PerceivedExertion).IsRequired().HasMaxLength(50).HasColumnName("PerceivedExertion");

            // Observations
            builder.Property(m => m.Observations).HasMaxLength(200).HasColumnName("Observations");

            // Relationship with Client
            builder.Property(m => m.ClientId).IsRequired().HasColumnName("ClientId");
            builder.HasOne(m => m.Client).WithMany().HasForeignKey(m => m.ClientId);
        }
    }
}
