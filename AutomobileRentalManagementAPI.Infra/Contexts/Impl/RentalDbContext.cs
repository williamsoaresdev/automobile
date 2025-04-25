using AutomobileRentalManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRentalManagementAPI.Infra.Contexts.Impl
{
    public class RentalDbContext(
    DbContextOptions<RentalDbContext> options)
    : DbContext(options)
    {
        //public DbSet<Motorcycle> Motorcycles { get; set; }
        //public DbSet<DeliveryPerson> DeliveryPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalDbContext).Assembly);
        }
    }
}
