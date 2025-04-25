using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoCommand : IRequest<UpdateLicensePlatePhotoResult>
    {
        public Guid NavigationId { get; set; }
        public string LicenseImageBase64 { get; set; } = null!;
    }
}