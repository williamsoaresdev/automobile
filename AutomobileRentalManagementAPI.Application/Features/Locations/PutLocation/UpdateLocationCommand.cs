using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation
{
    public class UpdateLocationCommand : IRequest<UpdatedLocationResult>
    {
        public Guid NavigationId { get; set; }
        public DateTime DevolutionDate { get; set; }
    }
}