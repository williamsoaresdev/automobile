namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Create
{
    public class CreateMotorcycleResponse
    {
        public Guid NavigationId { get; set; }
        public string identificador { get; init; } = null!;
        public int ano { get; init; }
        public string modelo { get; init; } = null!;
        public string placa { get; init; } = null!;
    }
}