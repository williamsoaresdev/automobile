using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation
{
    public class CreatedLocationResult
    {
        public Guid NavigationId { get; set; }
        public Guid IdDeliveryPerson { get; set; }
        public Guid IdMotorcycle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public LocationPlan Plan { get; set; }
    }
}