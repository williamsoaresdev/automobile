using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Put
{
    public class UpdateMotorcycleRequestValidator : AbstractValidator<UpdateMotorcycleRequest>
    {
        public UpdateMotorcycleRequestValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty();
            RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(8);
        }
    }
}