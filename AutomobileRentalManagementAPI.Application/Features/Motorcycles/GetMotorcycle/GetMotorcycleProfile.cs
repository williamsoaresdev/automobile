using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle
{
    public class GetMotorcycleProfile : Profile
    {
        public GetMotorcycleProfile()
        {
            CreateMap<Motorcycle, GetMotorcycleResult>();
        }
    }
}