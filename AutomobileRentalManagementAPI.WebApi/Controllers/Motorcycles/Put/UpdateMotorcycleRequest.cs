namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Put
{
    public class UpdateMotorcycleRequest
    {
        public Guid NavigationId { get; set; }
        public string LicensePlate { get; set; } = null!;
    }
}