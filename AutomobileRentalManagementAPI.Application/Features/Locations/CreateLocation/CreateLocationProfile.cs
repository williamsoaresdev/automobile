using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation
{
    public class CreateLocationProfile : Profile
    {
        public CreateLocationProfile()
        {
            CreateMap<CreateLocationCommand, Location>();
            CreateMap<Location, CreatedLocationResult>();
        }
    }
}