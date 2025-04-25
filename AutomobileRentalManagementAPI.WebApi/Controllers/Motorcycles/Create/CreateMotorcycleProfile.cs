using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Create
{
    public class CreateMotorcycleProfile : Profile
    {
        public CreateMotorcycleProfile()
        {
            CreateMap<CreateMotorcycleRequest, CreateMotorcycleCommand>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.identificador))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.ano))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.modelo))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.placa));

            CreateMap<CreateMotorcycleResponse, CreateMotorcycleResult>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.identificador))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.ano))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.modelo))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.placa));
        }
    }
}