using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Create
{
    public class CreateLocationRequestValidator : AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationRequestValidator()
        {
            RuleFor(x => x.entregador_id)
                .NotEmpty().WithMessage("Delivery person is required.");

            RuleFor(x => x.moto_id)
                .NotEmpty().WithMessage("Motorcycle is required.");

            RuleFor(x => x.data_inicio)
                .NotEmpty().WithMessage("Start date is required.")
                .Must(BeTheDayAfterToday).WithMessage("Start date must be the day after today.");

            RuleFor(x => x.data_termino)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.data_inicio).WithMessage("End date must be after start date.");

            RuleFor(x => x.data_previsao_termino)
                .NotEmpty().WithMessage("Estimated end date is required.")
                .Equal(x => x.data_termino).WithMessage("Estimated end date must match the end date.");

            RuleFor(x => x.plano)
                .IsInEnum().WithMessage("Invalid plan.");
        }

        private bool BeTheDayAfterToday(DateTime startDate)
        {
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);
            return startDate.Date == tomorrow;
        }
    }
}