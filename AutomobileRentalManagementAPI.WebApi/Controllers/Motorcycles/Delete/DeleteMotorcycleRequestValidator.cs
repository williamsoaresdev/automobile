using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Delete
{
    public class DeleteMotorcycleRequestValidator : AbstractValidator<DeleteMotorcycleRequest>
    {
        public DeleteMotorcycleRequestValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty().NotEqual(Guid.Empty).WithMessage("Id is required");
        }
    }
}
