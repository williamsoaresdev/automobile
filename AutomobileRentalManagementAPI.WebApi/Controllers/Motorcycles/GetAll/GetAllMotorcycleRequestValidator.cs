using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.GetAll
{
    public class GetAllMotorcyclesRequestValidator : AbstractValidator<GetAllMotorcyclesRequest>
    {
        public GetAllMotorcyclesRequestValidator()
        {
            RuleFor(x => x.LicensePlate)
                .MaximumLength(8)
                .When(x => !string.IsNullOrWhiteSpace(x.LicensePlate));
        }
    }
}
