using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Persistence.Configurations
{
    internal class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ClientID");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.OwnsOne(p => p.Address, a =>
            {
                a.Property(a => a.AddressLine1)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(false);

                a.Property(a => a.AddressLine2)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .IsUnicode(false);

                a.Property(a => a.Parish)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false);
            });
        }
    }
}