using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.DevolutionDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Devolution date is required.");
        }
    }
}