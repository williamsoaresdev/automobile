using AutomobileRentalManagementAPI.Domain;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AutomobileRentalManagementAPI.Infra.Repositories.Base
{
    public class MongoRepositoryBase<T> : IMongoRepositoryBase<T> where T : IDocument
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoRepositoryBase(MongoDbContextFactory factory, string collectionName)
        {
            _collection = factory.GetCollection<T>(collectionName);
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
