namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle
{
    public class GetMotorcycleResult
    {
        public Guid NavigationId { get; set; }
        public string Identifier { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
    }
}