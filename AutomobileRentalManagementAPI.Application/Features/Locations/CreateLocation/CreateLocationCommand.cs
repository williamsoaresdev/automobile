using AutomobileRentalManagementAPI.Domain.Enums;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation
{
    public sealed class CreateLocationCommand : IRequest<CreatedLocationResult>
    {
        public Guid IdDeliveryPerson { get; init; }
        public Guid IdMotorcycle { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime EstimatedEndDate { get; init; }
        public LocationPlan Plan { get; init; }
    }
}