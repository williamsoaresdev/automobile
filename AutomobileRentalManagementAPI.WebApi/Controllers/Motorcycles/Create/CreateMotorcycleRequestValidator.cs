using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Create
{
    public class CreateMotorcycleRequestValidator : AbstractValidator<CreateMotorcycleRequest>
    {
        public CreateMotorcycleRequestValidator()
        {
            RuleFor(motorcycle => motorcycle.identificador)
                .NotNull()
                .NotEmpty();

            RuleFor(motorcycle => motorcycle.ano)
                .NotNull()
                .NotEmpty();

            RuleFor(motorcycle => motorcycle.modelo)
                .NotNull()
                .NotEmpty();

            RuleFor(motorcycle => motorcycle.placa)
                .NotNull()
                .NotEmpty()
                .Length(8);
        }
    }
}