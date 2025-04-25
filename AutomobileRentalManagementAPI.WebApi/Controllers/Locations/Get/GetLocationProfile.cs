using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation;
using AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Get;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<GetLocationResponse, GetLocationResult>()
            .ForMember(dest => dest.NavigationId, opt => opt.MapFrom(src => Guid.Parse(src.identificador)))
            .ForMember(dest => dest.IdDeliveryPerson, opt => opt.MapFrom(src => Guid.Parse(src.entregador_id)))
            .ForMember(dest => dest.IdMotorcycle, opt => opt.MapFrom(src => Guid.Parse(src.moto_id)))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.data_inicio))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.data_termino))
            .ForMember(dest => dest.EstimatedEndDate, opt => opt.MapFrom(src => src.data_previsao_termino))
            .ForMember(dest => dest.Plan, opt => opt.Ignore())
            .ForMember(dest => dest.DailyValue, opt => opt.MapFrom(src => src.valor_diaria))
            .ForMember(dest => dest.DevolutionDate, opt => opt.MapFrom(src => src.data_devolucao))
            .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.valor_total));

        CreateMap<GetLocationResult, GetLocationResponse>()
            .ForMember(dest => dest.identificador, opt => opt.MapFrom(src => src.NavigationId.ToString()))
            .ForMember(dest => dest.entregador_id, opt => opt.MapFrom(src => src.IdDeliveryPerson.ToString()))
            .ForMember(dest => dest.moto_id, opt => opt.MapFrom(src => src.IdMotorcycle.ToString()))
            .ForMember(dest => dest.data_inicio, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.data_termino, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.data_previsao_termino, opt => opt.MapFrom(src => src.EstimatedEndDate))
            .ForMember(dest => dest.valor_diaria, opt => opt.MapFrom(src => src.DailyValue))
            .ForMember(dest => dest.data_devolucao, opt => opt.MapFrom(src => src.DevolutionDate))
            .ForMember(dest => dest.valor_total, opt => opt.MapFrom(src => src.TotalValue));
    }
}