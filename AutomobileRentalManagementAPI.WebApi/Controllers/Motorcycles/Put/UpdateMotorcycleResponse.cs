namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Put
{
    public class UpdateMotorcycleResponse
    {
        public Guid NavigationId { get; set; }
        public string identificador { get; set; } = null!;
        public int ano { get; set; }
        public string modelo { get; set; } = null!;
        public string placa { get; set; } = null!;
    }
}
