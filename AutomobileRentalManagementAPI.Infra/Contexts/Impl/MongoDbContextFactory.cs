using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AutomobileRentalManagementAPI.Infra.Contexts.Impl
{
    public class MongoDbContextFactory
    {
        private readonly IMongoDatabase _database;

        public MongoDbContextFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoConnection");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("automobile-rental");
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
    }
}
