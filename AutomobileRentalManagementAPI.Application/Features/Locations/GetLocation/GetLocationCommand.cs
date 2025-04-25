using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation
{
    public class GetLocationCommand : IRequest<GetLocationResult>
    {
        public Guid NavigationId { get; set; }
    }
}