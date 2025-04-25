using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson;
using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.Create
{
    public class CreateDeliveryPersonProfile : Profile
    {
        public CreateDeliveryPersonProfile()
        {
            CreateMap<CreateDeliveryPersonRequest, CreateDeliveryPersonCommand>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.identificador))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.nome))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.cnpj))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.data_nascimento))
                .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.numero_cnh))
                .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => ToEnum(src.tipo_cnh)))
                .ForMember(dest => dest.LicenseImageBase64, opt => opt.MapFrom(src => src.imagem_cnh));

            CreateMap<CreatedDeliveryPersonResult, CreateDeliveryPersonResponse>()
                .ForMember(dest => dest.identificador, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.nome, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.cnpj, opt => opt.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.data_nascimento, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.numero_cnh, opt => opt.MapFrom(src => src.LicenseNumber))
                .ForMember(dest => dest.tipo_cnh, opt => opt.MapFrom(src => ToDescription(src.LicenseType)))
                .ForMember(dest => dest.imagem_cnh, opt => opt.MapFrom(src => src.LicenseImageUrl));
        }

        private static CnhType ToEnum(string value)
        {
            if (value == "A") return CnhType.A;
            if (value == "B") return CnhType.B;
            if (value == "A+B") return CnhType.AB;

            throw new ArgumentException($"Invalid CNH type: {value}");
        }
        
        private static string ToDescription(CnhType value)
        {
            if (value == CnhType.A) return "A";
            if (value == CnhType.B) return "B";
            if (value == CnhType.AB) return "A+B";

            throw new ArgumentOutOfRangeException(nameof(value), $"Unexpected CNH type: {value}");
        }
    }
}