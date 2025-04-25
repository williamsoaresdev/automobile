using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle
{
    public class DeleteMotorcycleValidator : AbstractValidator<DeleteMotorcycleCommand>
    {
        public DeleteMotorcycleValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty().WithMessage("Id is required");
        }
    }
}