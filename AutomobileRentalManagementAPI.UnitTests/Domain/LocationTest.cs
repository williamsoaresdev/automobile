using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Domain
{
    public class LocationTest
    {
        [Fact(DisplayName = "Should return zero total value if DevolutionDate is null")]
        public void CalculateValues_WithoutDevolutionDate_ShouldSetTotalValueToZero()
        {
            // Arrange
            var location = new Location
            {
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(7),
                Plan = LocationPlan.SevenDays,
                DevolutionDate = null
            };

            // Act
            location.CalculateValues();

            // Assert
            Assert.Equal(30m, location.DailyValue); // Plan: SevenDays => 30
            Assert.Equal(0m, location.TotalValue);
        }

        [Fact(DisplayName = "Should calculate base value for on-time devolution")]
        public void CalculateValues_WithExactDevolutionDate_ShouldReturnExpectedTotal()
        {
            // Arrange
            var location = new Location
            {
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(15),
                Plan = LocationPlan.FifteenDays,
                DevolutionDate = DateTime.UtcNow.Date.AddDays(15)
            };

            // Act
            location.CalculateValues();

            // Assert
            Assert.Equal(28m, location.DailyValue); // Plan: FifteenDays => 28
            Assert.Equal(28m * 15, location.TotalValue);
        }

        [Fact(DisplayName = "Should apply 20% fine for early devolution on 7-day plan")]
        public void CalculateValues_EarlyDevolution_SevenDayPlan_ShouldApplyFine()
        {
            // Arrange
            var location = new Location
            {
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(7),
                Plan = LocationPlan.SevenDays,
                DevolutionDate = DateTime.UtcNow.Date.AddDays(5)
            };

            // Act
            location.CalculateValues();

            // Assert
            var daily = 30m;
            var baseValue = 7 * daily;
            var unusedDays = 2;
            var unusedValue = unusedDays * daily;
            var fine = unusedValue * 0.2m;
            var expectedTotal = baseValue - unusedValue + fine;

            Assert.Equal(daily, location.DailyValue);
            Assert.Equal(expectedTotal, location.TotalValue);
        }

        [Fact(DisplayName = "Should apply 40% fine for early devolution on 15-day plan")]
        public void CalculateValues_EarlyDevolution_FifteenDayPlan_ShouldApplyFine()
        {
            // Arrange
            var location = new Location
            {
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(15),
                Plan = LocationPlan.FifteenDays,
                DevolutionDate = DateTime.UtcNow.Date.AddDays(10)
            };

            // Act
            location.CalculateValues();

            // Assert
            var daily = 28m;
            var baseValue = 15 * daily;
            var unusedDays = 5;
            var unusedValue = unusedDays * daily;
            var fine = unusedValue * 0.4m;
            var expectedTotal = baseValue - unusedValue + fine;

            Assert.Equal(daily, location.DailyValue);
            Assert.Equal(expectedTotal, location.TotalValue);
        }

        [Fact(DisplayName = "Should add R$50/day for late devolution")]
        public void CalculateValues_LateDevolution_ShouldAddPenaltyPerDay()
        {
            // Arrange
            var location = new Location
            {
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(30),
                Plan = LocationPlan.ThirtyDays,
                DevolutionDate = DateTime.UtcNow.Date.AddDays(33) // 3 days late
            };

            // Act
            location.CalculateValues();

            // Assert
            var daily = 22m;
            var baseValue = 30 * daily;
            var extra = 3 * 50m;
            var expectedTotal = baseValue + extra;

            Assert.Equal(daily, location.DailyValue);
            Assert.Equal(expectedTotal, location.TotalValue);
        }

        [Theory(DisplayName = "Should apply fine for early devolution according to plan")]
        [InlineData(LocationPlan.SevenDays, 30, 2, 0.2)]     // 2 days earlier, 20% fine
        [InlineData(LocationPlan.FifteenDays, 28, 5, 0.4)]   // 5 days earlier, 40% fine
        public void CalculateValues_EarlyDevolution_ShouldApplyCorrectFine(LocationPlan plan, decimal dailyValue, int unusedDays, double finePercentage)
        {
            // Arrange
            var startDate = DateTime.UtcNow.Date;
            var estimatedEndDate = startDate.AddDays((int)plan);
            var devolutionDate = estimatedEndDate.AddDays(-unusedDays);

            var location = new Location
            {
                StartDate = startDate,
                EstimatedEndDate = estimatedEndDate,
                Plan = plan,
                DevolutionDate = devolutionDate
            };

            // Act
            location.CalculateValues();

            // Assert
            var baseValue = (int)plan * dailyValue;
            var unusedValue = unusedDays * dailyValue;
            var fine = unusedValue * (decimal)finePercentage;
            var expectedTotal = baseValue - unusedValue + fine;

            Assert.Equal(dailyValue, location.DailyValue);
            Assert.Equal(expectedTotal, location.TotalValue);
        }

        [Fact(DisplayName = "Should return 0 values for unrecognized plan")]
        public void CalculateValues_InvalidPlan_ShouldReturnZeroValues()
        {
            // Arrange
            var invalidPlan = (LocationPlan)999;
            var startDate = DateTime.UtcNow.Date;
            var estimatedEndDate = startDate.AddDays(10);
            var devolutionDate = estimatedEndDate;

            var location = new Location
            {
                StartDate = startDate,
                EstimatedEndDate = estimatedEndDate,
                Plan = invalidPlan,
                DevolutionDate = devolutionDate
            };

            // Act
            location.CalculateValues();

            // Assert
            Assert.Equal(0m, location.DailyValue);
            Assert.Equal(0m, location.TotalValue);
        }

        [Theory(DisplayName = "Should discount unused days for long plans with early return and no fine")]
        [InlineData(LocationPlan.FortyFiveDays, 20, 5)]
        [InlineData(LocationPlan.FiftyDays, 18, 5)]
        public void CalculateValues_EarlyDevolution_LongPlans_ShouldDiscountUnusedDays(LocationPlan plan, decimal dailyValue, int unusedDays)
        {
            // Arrange
            var startDate = DateTime.UtcNow.Date;
            var estimatedEndDate = startDate.AddDays((int)plan);
            var devolutionDate = estimatedEndDate.AddDays(-unusedDays); // devolução antecipada

            var location = new Location
            {
                StartDate = startDate,
                EstimatedEndDate = estimatedEndDate,
                Plan = plan,
                DevolutionDate = devolutionDate
            };

            // Act
            location.CalculateValues();

            // Assert
            var plannedDays = (estimatedEndDate - startDate).Days;
            var baseValue = plannedDays * dailyValue;
            var unusedValue = unusedDays * dailyValue;
            var expectedTotal = baseValue - unusedValue; // sem multa, apenas desconto

            Assert.Equal(dailyValue, location.DailyValue);
            Assert.Equal(expectedTotal, location.TotalValue);
        }

    }
}
