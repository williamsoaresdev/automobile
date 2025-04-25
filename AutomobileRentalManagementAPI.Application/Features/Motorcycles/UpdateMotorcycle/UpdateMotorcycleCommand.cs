using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle
{
    public class UpdateMotorcycleCommand : IRequest<UpdateMotorcycleResult>
    {
        public Guid NavigationId { get; set; }
        public string LicensePlate { get; set; } = null!;
    }
}