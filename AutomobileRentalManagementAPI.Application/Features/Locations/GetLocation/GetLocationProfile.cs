using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation
{
    public class GetLocationProfile : Profile
    {
        public GetLocationProfile()
        {
            CreateMap<Location, GetLocationResult>();
        }
    }
}