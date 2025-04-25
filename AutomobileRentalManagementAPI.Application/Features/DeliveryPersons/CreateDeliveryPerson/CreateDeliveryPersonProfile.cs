using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson
{
    public class CreateDeliveryPersonProfile : Profile
    {
        public CreateDeliveryPersonProfile()
        {
            CreateMap<CreateDeliveryPersonCommand, DeliveryPerson>();
            
            CreateMap<DeliveryPerson, CreatedDeliveryPersonResult>();
        }
    }
}