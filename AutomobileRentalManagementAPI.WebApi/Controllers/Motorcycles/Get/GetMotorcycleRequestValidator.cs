using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Get
{
    public class GetMotorcycleRequestValidator : AbstractValidator<GetMotorcycleRequest>
    {
        public GetMotorcycleRequestValidator()
        {
            RuleFor(x => x.NavigationId).NotEmpty().NotEqual(Guid.Empty).WithMessage("Id is required");
        }
    }
}