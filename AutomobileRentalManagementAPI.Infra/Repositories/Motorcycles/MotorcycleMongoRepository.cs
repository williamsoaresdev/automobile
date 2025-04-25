using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using AutomobileRentalManagementAPI.Infra.Repositories.Base;

namespace AutomobileRentalManagementAPI.Infra.Repositories.Motorcycles
{
    public class MotorcycleMongoRepository : MongoRepositoryBase<MotorcycleMongo>, IMotorcycleMongoRepository
    {
        public MotorcycleMongoRepository(MongoDbContextFactory factory)
            : base(factory, "Motorcycles")
        {
        }
    }
}
