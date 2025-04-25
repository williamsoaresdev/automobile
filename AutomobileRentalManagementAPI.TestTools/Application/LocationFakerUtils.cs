using AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using Bogus;

namespace AutomobileRentalManagementAPI.TestTools.Application
{
    public static class LocationFakerUtils
    {
        public static CreateLocationCommand GenerateValidCommand()
        {
            return CreateValidCommand().Generate();
        }

        public static Location GenerateValidDomainEntity()
        {
            return CreateValidDomainEntity().Generate();
        }

        public static List<Location> GenerateValidDomainEntity(int listSize)
        {
            return CreateValidDomainEntity().Generate(listSize);
        }

        private static Faker<CreateLocationCommand> CreateValidCommand()
        {
            var planOptions = new[]
            {
                LocationPlan.SevenDays,
                LocationPlan.FifteenDays,
                LocationPlan.ThirtyDays,
                LocationPlan.FortyFiveDays,
                LocationPlan.FiftyDays
            };

            return new Faker<CreateLocationCommand>()
                .RuleFor(x => x.IdDeliveryPerson, f => f.Random.Guid())
                .RuleFor(x => x.IdMotorcycle, f => f.Random.Guid())
                .RuleFor(x => x.StartDate, _ => DateTime.UtcNow.Date.AddDays(1)) // obrigatory the "day after today"
                .RuleFor(x => x.Plan, f => f.PickRandom(planOptions))
                .RuleFor(x => x.EndDate, (f, x) => x.StartDate.AddDays((int)x.Plan)) // end > start
                .RuleFor(x => x.EstimatedEndDate, (f, x) => x.EndDate); // estimated == end
        }

        private static Faker<Location> CreateValidDomainEntity()
        {
            return new Faker<Location>()
                .RuleFor(x => x.IdDeliveryPerson, f => f.Random.Guid())
                .RuleFor(x => x.IdMotorcycle, f => f.Random.Guid())
                .RuleFor(x => x.StartDate, f => f.Date.PastOffset(1).UtcDateTime.Date)
                .RuleFor(x => x.Plan, f => f.PickRandom<LocationPlan>())
                .RuleFor(x => x.EstimatedEndDate, (f, c) => c.StartDate.AddDays((int)c.Plan))
                .RuleFor(x => x.EndDate, (f, c) => c.EstimatedEndDate)
                .RuleFor(x => x.DevolutionDate, (f, c) => c.EstimatedEndDate.AddDays(f.Random.Int(-2, 5)))
                .RuleFor(x => x.DailyValue, f => f.Finance.Amount(15, 50))
                .RuleFor(x => x.TotalValue, f => f.Finance.Amount(100, 1000));
        }
    }
}
