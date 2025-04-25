using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles
{
    public class GetAllMotorcyclesCommand : IRequest<GetAllMotorcyclesResult>
    {
        public string? LicensePlate { get; set; }
    }
}
