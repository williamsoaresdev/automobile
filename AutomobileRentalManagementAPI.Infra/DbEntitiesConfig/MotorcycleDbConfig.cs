using AutomobileRentalManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomobileRentalManagementAPI.Infra.DbEntitiesConfig
{
    public class MotorcycleDbConfig : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable(nameof(Motorcycle));

            builder.HasKey(x => x.NavigationId);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
       
            builder.Property(x => x.NavigationId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Identifier).HasColumnType("text").IsRequired();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Model).HasColumnType("text").IsRequired();
            builder.Property(x => x.LicensePlate).HasColumnType("text").IsRequired();
        }
    }
}