using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Entities;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoProfile : Profile
    {
        public UpdateLicensePlatePhotoProfile()
        {
            CreateMap<UpdateLicensePlatePhotoCommand, DeliveryPerson>();

            CreateMap<DeliveryPerson, UpdateLicensePlatePhotoResult>();
        }
    }
}