using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Put
{
    public class UpdateLocationRequestValidator : AbstractValidator<UpdateLocationRequest>
    {
        public UpdateLocationRequestValidator()
        {
            RuleFor(x => x.data_devolucao)
                .NotNull()
                .NotEmpty()
                .WithMessage("Devolution date is required.");
        }
    }
}