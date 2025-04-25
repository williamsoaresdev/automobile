using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using AutomobileRentalManagementAPI.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRentalManagementAPI.Infra.Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(RentalDbContext db) : base(db) { }

        public async Task<bool> HasAnyWithMotorcycleAsync(Guid motorcycleNavigationId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(l => l.IdMotorcycle == motorcycleNavigationId, cancellationToken);
        }
    }
}
