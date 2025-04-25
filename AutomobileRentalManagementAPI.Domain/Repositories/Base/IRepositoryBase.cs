using AutomobileRentalManagementAPI.Domain.Entities.Base;

namespace AutomobileRentalManagementAPI.Domain.Repositories.Base
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken, bool isActive = true);
        Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken);
    }
}