using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.IdDeliveryPerson)
                .NotEmpty().WithMessage("Delivery person is required.");

            RuleFor(x => x.IdMotorcycle)
                .NotEmpty().WithMessage("Motorcycle is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .Must(BeTheDayAfterToday).WithMessage("Start date must be the day after today.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");

            RuleFor(x => x.EstimatedEndDate)
                .NotEmpty().WithMessage("Estimated end date is required.")
                .Equal(x => x.EndDate).WithMessage("Estimated end date must match the end date.");

            RuleFor(x => x.Plan)
                .IsInEnum().WithMessage("Invalid plan.");
        }

        private bool BeTheDayAfterToday(DateTime startDate)
        {
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);
            return startDate.Date == tomorrow;
        }
    }

}