using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle
{
    public class GetMotorcycleCommand : IRequest<GetMotorcycleResult>
    {
        public Guid NavigationId { get; set; }

        public GetMotorcycleCommand(Guid navigationId)
        {
            NavigationId = navigationId;
        }
    }
}