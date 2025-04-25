using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle
{
    public class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
    {
        public UpdateMotorcycleCommandValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty();
            RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(8);
        }
    }
}