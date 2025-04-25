using AutomobileRentalManagementAPI.CrossCutting.Util;
using AutomobileRentalManagementAPI.Domain.Enums;
using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.Create
{
    public class CreateDeliveryPersonRequestValidator : AbstractValidator<CreateDeliveryPersonRequest>
    {
        public CreateDeliveryPersonRequestValidator()
        {
            RuleFor(x => x.identificador)
                .NotEmpty().WithMessage("Identifier is required.");

            RuleFor(x => x.nome)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.cnpj)
                .NotEmpty().WithMessage("CNPJ is required.")
                .Must(ValidationUtil.IsValidCnpj).WithMessage("CNPJ is invalid.");

            RuleFor(x => x.data_nascimento)
                .NotEmpty().WithMessage("Birthdate is required.")
                .LessThan(DateTime.UtcNow).WithMessage("Birthdate must be in the past.");

            RuleFor(x => x.numero_cnh)
                .NotEmpty().WithMessage("Driver's license number is required.")
                .Length(11).WithMessage("Driver's license number must be 11 digits.")
                .Matches(@"^\d+$").WithMessage("Driver's license number must contain only digits.");

            RuleFor(x => x.tipo_cnh)
               .NotNull().NotEmpty().WithMessage("Driver's license type is required.")
               .Must(value => ValidationUtil.IsValidEnumDescription<CnhType>(value))
               .WithMessage("Driver's license type is invalid. Allowed values: A, B, A+B.");
            
        }
    }
}