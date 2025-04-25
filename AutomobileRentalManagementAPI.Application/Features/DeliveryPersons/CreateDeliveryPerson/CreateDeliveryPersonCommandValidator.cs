using AutomobileRentalManagementAPI.CrossCutting.Util;
using AutomobileRentalManagementAPI.Domain.Enums;
using FluentValidation;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson
{
    public class CreateDeliveryPersonCommandValidator : AbstractValidator<CreateDeliveryPersonCommand>
    {
        public CreateDeliveryPersonCommandValidator()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty().WithMessage("Identifier is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ is required.")
                .Must(ValidationUtil.IsValidCnpj).WithMessage("CNPJ is invalid.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("Driver's license number is required.")
                .Length(11).WithMessage("Driver's license number must be 11 digits.")
                .Matches(@"^\d+$").WithMessage("Driver's license number must contain only digits.");

            RuleFor(x => x.LicenseType)
               .NotNull().WithMessage("Driver's license type is required.")
               .Must(value => Enum.IsDefined(typeof(CnhType), value))
               .WithMessage("Driver's license type is invalid. Allowed values: A, B, A+B.");

            RuleFor(x => x.LicenseImageBase64)
                .NotEmpty().WithMessage("Driver's license image is required.");
        }
    }
}
