using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles
{
    public class GetAllMotorcyclesValidator : AbstractValidator<GetAllMotorcyclesCommand>
    {
        public GetAllMotorcyclesValidator()
        {
            RuleFor(x => x.LicensePlate)
                .MaximumLength(8)
                .When(x => !string.IsNullOrWhiteSpace(x.LicensePlate));
        }
    }
}
