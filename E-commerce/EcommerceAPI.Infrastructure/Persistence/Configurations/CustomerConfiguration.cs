using EcommerceAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceAPI.Infrastructure.Persistence.Configurations
{
       public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
       {
              public void Configure(EntityTypeBuilder<Customer> builder)
              {
                     builder.HasKey(c => c.Id);

                     builder.Property(c => c.Name)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(c => c.Cpf)
                            .IsRequired()
                            .HasMaxLength(11);       

                     builder.Property(c => c.Email)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(c => c.PasswordHash)
                                 .IsRequired()
                                 .HasMaxLength(200);

                     builder.Property(c => c.Phone)
                            .HasMaxLength(20);

                     builder.Property(c => c.Address)
                            .HasMaxLength(200);

                     // Relação Customer -> Orders
                     builder.HasMany(c => c.Orders)
                            .WithOne(o => o.Customer)
                            .HasForeignKey(o => o.CustomerId);
              }
       }
}
