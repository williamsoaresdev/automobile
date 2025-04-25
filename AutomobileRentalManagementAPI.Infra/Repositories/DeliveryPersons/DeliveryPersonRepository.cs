using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using AutomobileRentalManagementAPI.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRentalManagementAPI.Infra.Repositories.DeliveryPersons
{
    public class DeliveryPersonRepository : RepositoryBase<DeliveryPerson>, IDeliveryPersonRepository
    {
        public DeliveryPersonRepository(RentalDbContext db) : base(db)
        {
        }

        public async Task<bool> HasPreviousRegister(string cnpj, string cnh)
        {
            return await _dbSet.AsNoTracking().AnyAsync(x => x.Cnpj == cnpj || x.LicenseNumber == cnh);
        }
    }
}