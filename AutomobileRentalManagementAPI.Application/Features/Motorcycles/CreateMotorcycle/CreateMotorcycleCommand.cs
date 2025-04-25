using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle
{
    public class CreateMotorcycleCommand : IRequest<CreateMotorcycleResult>
    {
        public Guid NavigationId { get; set; }
        public string Identifier { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
    }
}