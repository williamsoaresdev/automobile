using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle;
using AutomobileRentalManagementAPI.Domain.Entities;
using Bogus;

namespace AutomobileRentalManagementAPI.TestTools.Application
{
    public static class MotorcycleFakerUtils
    {
        public static CreateMotorcycleCommand GenerateValidCreateCommand()
        {
            return CreateValidCreateCommand().Generate();
        }

        public static UpdateMotorcycleCommand GenerateValidUpdateCommand()
        {
            return CreateValidUpdateCommand().Generate();
        }

        public static Motorcycle GenerateValidDomainEntity()
        {
            return CreateValidDomainEntity().Generate();
        }

        public static List<Motorcycle> GenerateValidDomainEntity(int count)
        {
            return CreateValidDomainEntity().Generate(count);
        }

        public static CreateMotorcycleResult GenerateValidCreateResult()
        {
            return CreateValidCreateResult().Generate();
        }

        public static UpdateMotorcycleResult GenerateValidUpdateResult()
        {
            return CreateValidUpdateResult().Generate();
        }

        // Faker Factories

        private static Faker<CreateMotorcycleCommand> CreateValidCreateCommand()
        {
            return new Faker<CreateMotorcycleCommand>()
                .RuleFor(c => c.Identifier, f => f.Vehicle.Vin())
                .RuleFor(c => c.Model, f => f.Vehicle.Model())
                .RuleFor(c => c.Year, f => f.Date.Past(15).Year)
                .RuleFor(c => c.LicensePlate, f => f.Random.Replace("???-####").ToUpper());
        }

        private static Faker<UpdateMotorcycleCommand> CreateValidUpdateCommand()
        {
            return new Faker<UpdateMotorcycleCommand>()
                .RuleFor(c => c.NavigationId, f => Guid.NewGuid())
                .RuleFor(c => c.LicensePlate, f => f.Random.Replace("???-####").ToUpper());
        }

        private static Faker<Motorcycle> CreateValidDomainEntity()
        {
            return new Faker<Motorcycle>()
                .RuleFor(e => e.NavigationId, f => Guid.NewGuid())
                .RuleFor(e => e.Identifier, f => f.Vehicle.Vin())
                .RuleFor(e => e.Model, f => f.Vehicle.Model())
                .RuleFor(e => e.Year, f => f.Date.Past(15).Year)
                .RuleFor(e => e.LicensePlate, f => f.Random.Replace("???-####").ToUpper());
        }

        private static Faker<CreateMotorcycleResult> CreateValidCreateResult()
        {
            return new Faker<CreateMotorcycleResult>()
                .RuleFor(r => r.NavigationId, f => Guid.NewGuid())
                .RuleFor(r => r.Identifier, f => f.Vehicle.Vin())
                .RuleFor(r => r.Model, f => f.Vehicle.Model())
                .RuleFor(r => r.Year, f => f.Date.Past(15).Year)
                .RuleFor(r => r.LicensePlate, f => f.Random.Replace("???-####").ToUpper());
        }

        private static Faker<UpdateMotorcycleResult> CreateValidUpdateResult()
        {
            return new Faker<UpdateMotorcycleResult>()
                .RuleFor(r => r.NavigationId, f => Guid.NewGuid())
                .RuleFor(r => r.Identifier, f => f.Vehicle.Vin())
                .RuleFor(r => r.Model, f => f.Vehicle.Model())
                .RuleFor(r => r.Year, f => f.Date.Past(15).Year)
                .RuleFor(r => r.LicensePlate, f => f.Random.Replace("???-####").ToUpper());
        }
    }
}
