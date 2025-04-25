using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.GetAll
{
    public class GetAllMotorcyclesProfile : Profile
    {
        public GetAllMotorcyclesProfile()
        {
            CreateMap<GetAllMotorcyclesRequest, GetAllMotorcyclesCommand>();
            CreateMap<MotorcycleItem, GetAllMotorcyclesResponse>()
                .ForMember(dest => dest.identificador, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.ano, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.modelo, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.placa, opt => opt.MapFrom(src => src.LicensePlate));
        }
    }
}
