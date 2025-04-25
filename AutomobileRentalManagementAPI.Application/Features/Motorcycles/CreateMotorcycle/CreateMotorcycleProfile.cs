using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle
{
    public class CreateMotorcycleProfile : Profile
    {
        public CreateMotorcycleProfile()
        {
            CreateMap<Motorcycle, CreateMotorcycleResult>();
            CreateMap<CreateMotorcycleCommand, Motorcycle>();
        }
    }
}