using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Put
{
    public class UpdateLocationProfile : Profile
    {
        public UpdateLocationProfile()
        {
            CreateMap<UpdateLocationRequest, UpdateLocationCommand>()
                .ForMember(dest => dest.DevolutionDate, opt => opt.MapFrom(src => src.data_devolucao));


            CreateMap<UpdatedLocationResult, UpdateLocationResponse>();
        }
    }
}