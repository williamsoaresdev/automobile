using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation
{
    public class UpdatedLocationResult
    {
        public Guid NavigationId { get; set; }
        public Guid IdDeliveryPerson { get; set; }
        public Guid IdMotorcycle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public LocationPlan Plan { get; set; }
        public decimal DailyValue { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public decimal? TotalValue { get; set; }
    }
}