using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Put
{
    public class UpdateMotorcycleProfile : Profile
    {
        public UpdateMotorcycleProfile()
        {
            CreateMap<UpdateMotorcycleRequest, UpdateMotorcycleCommand>();
            CreateMap<UpdateMotorcycleResult, UpdateMotorcycleResponse>()
                .ForMember(dest => dest.identificador, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.ano, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.modelo, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.placa, opt => opt.MapFrom(src => src.LicensePlate));
        }
    }
}