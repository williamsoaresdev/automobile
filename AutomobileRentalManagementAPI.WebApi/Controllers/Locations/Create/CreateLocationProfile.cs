using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Create
{
    public class CreateLocationProfile : Profile
    {
        public CreateLocationProfile()
        {
            CreateMap<CreateLocationRequest, CreateLocationCommand>()
                .ForMember(dest => dest.IdDeliveryPerson, opt => opt.MapFrom(src => Guid.Parse(src.entregador_id)))
                .ForMember(dest => dest.IdMotorcycle, opt => opt.MapFrom(src => Guid.Parse(src.moto_id)))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.data_inicio))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.data_termino))
                .ForMember(dest => dest.EstimatedEndDate, opt => opt.MapFrom(src => src.data_previsao_termino))
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.plano));


            CreateMap<CreateLocationResponse, CreatedLocationResult>();
        }
    }
}