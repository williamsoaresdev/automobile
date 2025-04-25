using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle
{
    public class GetMotorcycleValidator : AbstractValidator<GetMotorcycleCommand>
    {
        public GetMotorcycleValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty().WithMessage("Id is required");
        }
    }
}
