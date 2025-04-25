using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle
{
    public class UpdateMotorcycleProfile : Profile
    {
        public UpdateMotorcycleProfile()
        {
            CreateMap<Motorcycle, UpdateMotorcycleResult>();
        }
    }
}