using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle
{
    public class DeleteMotorcycleCommand : IRequest<Unit>
    {
        public Guid NavigationId { get; set; }

        public DeleteMotorcycleCommand(Guid navigationId)
        {
            NavigationId = navigationId;
        }
    }
}