using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Base;

namespace AutomobileRentalManagementAPI.Domain.Repositories
{
    public interface ILocationRepository : IRepositoryBase<Location>
    {
        Task<bool> HasAnyWithMotorcycleAsync(Guid motorcycleNavigationId, CancellationToken cancellationToken = default);
    }
}
