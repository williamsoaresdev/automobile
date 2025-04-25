using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles
{
    public class GetAllMotorcyclesProfile : Profile
    {
        public GetAllMotorcyclesProfile()
        {
            CreateMap<Motorcycle, MotorcycleItem>();
        }
    }
}
