using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoCommandValidator : AbstractValidator<UpdateLicensePlatePhotoCommand>
    {
        public UpdateLicensePlatePhotoCommandValidator()
        {
            RuleFor(licensePlate => licensePlate.LicenseImageBase64)
               .NotNull()
               .NotEmpty()
               .WithMessage("The cnh photo cannot be empty.");
        }
    }
}