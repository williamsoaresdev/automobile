using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using Bogus;

namespace AutomobileRentalManagementAPI.TestTools.Application
{
    public static class DeliveryPersonFakerUtils
    {
        private static readonly string[] ValidCnpjs = new[]
        {
            "11222333000181",
            "67217631000138",
            "15546101000130",
        };

        public static CreateDeliveryPersonCommand GenerateValidCommand()
        {
            return CreateValidCommand().Generate();
        }

        public static DeliveryPerson GenerateValidDomainEntity()
        {
            return CreateValidDomainEntity().Generate();
        }

        private static Faker<CreateDeliveryPersonCommand> CreateValidCommand()
        {
            var licenseTypeArray = new[] { 
                CnhType.A, 
                CnhType.B,
                CnhType.AB
            };

            var faker = new Faker<CreateDeliveryPersonCommand>()
                .RuleFor(c => c.Identifier, f => f.Random.Guid().ToString())
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Cnpj, f => f.PickRandom(ValidCnpjs))
                .RuleFor(c => c.BirthDate, f => f.Date.Past(30, DateTime.UtcNow.AddYears(-18)))
                .RuleFor(c => c.LicenseNumber, f => f.Random.ReplaceNumbers("###########")) // 11 digits
                .RuleFor(c => c.LicenseType, f => f.PickRandom(licenseTypeArray))
                .RuleFor(c => c.LicenseImageBase64, f => Convert.ToBase64String(f.Random.Bytes(100)));

            return faker;
        }

        private static Faker<DeliveryPerson> CreateValidDomainEntity()
        {
            var faker = new Faker<DeliveryPerson>()
                .RuleFor(e => e.Identifier, f => f.Random.Guid().ToString())
                .RuleFor(e => e.Name, f => f.Name.FullName())
                .RuleFor(e => e.Cnpj, f => f.PickRandom(ValidCnpjs))
                .RuleFor(e => e.BirthDate, f => f.Date.Past(30, DateTime.UtcNow.AddYears(-18)))
                .RuleFor(e => e.LicenseNumber, f => f.Random.ReplaceNumbers("###########"))
                .RuleFor(e => e.LicenseType, f => f.PickRandom<CnhType>())
                .RuleFor(e => e.LicenseImageUrl, f => f.Internet.Avatar());

            return faker;
        }
    }
}