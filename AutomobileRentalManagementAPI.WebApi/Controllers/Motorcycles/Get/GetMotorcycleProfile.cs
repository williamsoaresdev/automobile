using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Get
{
    public class GetMotorcycleProfile : Profile
    {
        public GetMotorcycleProfile()
        {
            CreateMap<GetMotorcycleRequest, GetMotorcycleCommand>();

            CreateMap<GetMotorcycleResult, GetMotorcycleResponse>()
                .ForMember(dest => dest.identificador, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.ano, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.modelo, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.placa, opt => opt.MapFrom(src => src.LicensePlate));
        }
    }
}