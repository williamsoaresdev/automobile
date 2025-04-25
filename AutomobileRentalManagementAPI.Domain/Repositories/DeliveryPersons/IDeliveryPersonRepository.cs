using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Base;

namespace AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons
{
    public interface IDeliveryPersonRepository : IRepositoryBase<DeliveryPerson>
    {
        public Task<bool> HasPreviousRegister(string cnpj, string cnh);
    }
}