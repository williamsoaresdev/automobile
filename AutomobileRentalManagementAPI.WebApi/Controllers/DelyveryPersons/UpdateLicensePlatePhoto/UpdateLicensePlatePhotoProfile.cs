using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoProfile : Profile
    {
        public UpdateLicensePlatePhotoProfile()
        {
            CreateMap<UpdateLicensePlatePhotoRequest, UpdateLicensePlatePhotoCommand>()
                .ForMember(dest => dest.LicenseImageBase64, opt => opt.MapFrom(src => src.imagem_cnh));
            
            CreateMap<UpdateLicensePlatePhotoResult, UpdateLicensePlatePhotoResponse>();
        }
    }
}