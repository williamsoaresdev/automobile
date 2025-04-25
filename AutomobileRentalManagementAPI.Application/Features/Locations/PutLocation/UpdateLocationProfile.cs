using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation
{
    public class UpdateLocationProfile : Profile
    {
        public UpdateLocationProfile()
        {
            CreateMap<Location, UpdatedLocationResult>();
        }
    }
}