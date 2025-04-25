using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoRequestValidator : AbstractValidator<UpdateLicensePlatePhotoRequest>
    {
        public UpdateLicensePlatePhotoRequestValidator()
        {
            RuleFor(x => x.imagem_cnh)
                .NotNull()
                .NotEmpty()
                .WithMessage("Driver's license image is required.");
        }
    }
}