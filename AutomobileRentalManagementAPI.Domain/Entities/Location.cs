using AutomobileRentalManagementAPI.Domain.Entities.Base;
using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.Domain.Entities
{
    public class Location : BaseEntity
    {
        public Guid IdDeliveryPerson { get; set; }
        public Guid IdMotorcycle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public LocationPlan Plan { get; set; }
        public decimal DailyValue { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public decimal? TotalValue { get; set; }

        public void CalculateValues()
        {
            DailyValue = GetDailyValueByPlan();
            TotalValue = CalculateTotalValue();
        }

        private decimal GetDailyValueByPlan()
        {
            return Plan switch
            {
                LocationPlan.SevenDays => 30m,
                LocationPlan.FifteenDays => 28m,
                LocationPlan.ThirtyDays => 22m,
                LocationPlan.FortyFiveDays => 20m,
                LocationPlan.FiftyDays => 18m,
                _ => 0m
            };
        }

        private decimal CalculateTotalValue()
        {
            if (DevolutionDate == null)
                return 0m;

            decimal totalValue = 0m;
            int plannedDays = (EstimatedEndDate - StartDate).Days;
            int actualDays = (DevolutionDate.Value - StartDate).Days;

            // Base value of the contracted plan
            totalValue = plannedDays * DailyValue;

            // Checks for early devolution
            if (DevolutionDate < EstimatedEndDate)
            {
                int unusedDays = (EstimatedEndDate - DevolutionDate.Value).Days;
                decimal unusedValue = unusedDays * DailyValue;
                decimal fine = 0m;

                // Applies early devolution fine
                if (Plan == LocationPlan.SevenDays)
                {
                    fine = unusedValue * 0.2m; // 20% fine
                }
                else if (Plan == LocationPlan.FifteenDays)
                {
                    fine = unusedValue * 0.4m; // 40% fine
                }

                totalValue -= unusedValue;
                totalValue += fine;
            }
            // Checks for late devolution
            else if (DevolutionDate > EstimatedEndDate)
            {
                int extraDays = (DevolutionDate.Value - EstimatedEndDate).Days;
                totalValue += extraDays * 50m; // R$50 per additional day
            }

            return totalValue;
        }

    }
}