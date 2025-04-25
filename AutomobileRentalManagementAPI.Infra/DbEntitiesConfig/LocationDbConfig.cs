using AutomobileRentalManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomobileRentalManagementAPI.Infra.DbEntitiesConfig
{
    public class LocationDbConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(nameof(Location));

            builder.HasKey(d => d.NavigationId);
            
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.NavigationId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.EstimatedEndDate).IsRequired();
            builder.Property(x => x.Plan).IsRequired();

            builder.HasOne<DeliveryPerson>()
                   .WithMany()
                   .HasForeignKey(x => x.IdDeliveryPerson)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Motorcycle>()
                   .WithMany()
                   .HasForeignKey(x => x.IdMotorcycle)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
