using AutomobileRentalManagementAPI.Domain.Entities.Base;
using AutomobileRentalManagementAPI.Domain.Repositories.Base;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRentalManagementAPI.Infra.Repositories.Base
{
    public class RepositoryBase<T>(RentalDbContext db) : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet = db.Set<T>();

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken, bool isActive = true)
        {
            await db.AddAsync(entity);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                await db.Set<T>().AddAsync(entity);
            }


            await db.SaveChangesAsync();

            return entities;
        }

        public async Task UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            foreach (var item in entities)
            {
                db.Entry(item).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var find = await db.Set<T>().FindAsync(id);

            if (find != null)
                db.Entry(find).State = EntityState.Detached;

            return find;
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken) => await db.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            if (entities != null)
            {
                db.Set<T>().RemoveRange(entities);
                await db.SaveChangesAsync();
            }
        }
    }
}