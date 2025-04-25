using AutomobileRentalManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomobileRentalManagementAPI.Infra.DbEntitiesConfig
{
    public class DeliveryPersonDbConfig : IEntityTypeConfiguration<DeliveryPerson>
    {
        public void Configure(EntityTypeBuilder<DeliveryPerson> builder)
        {
            builder.ToTable(nameof(DeliveryPerson));

            builder.HasKey(d => d.NavigationId);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.NavigationId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(d => d.Identifier)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.Cnpj)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.BirthDate)
                .IsRequired();

            builder.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.LicenseType)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.LicenseImageUrl)
                .IsRequired()
                .HasColumnType("text");

            // Unique key
            builder.HasIndex(d => d.Cnpj)
                .IsUnique();

            builder.HasIndex(d => d.LicenseNumber)
                .IsUnique();
        }
    }
}