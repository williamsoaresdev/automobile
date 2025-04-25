namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Create
{
    public sealed class CreateMotorcycleRequest
    {
        public string identificador { get; init; } = null!;
        public int ano { get; init; }
        public string modelo { get; init; } = null!;
        public string placa { get; init; } = null!;
    }
}