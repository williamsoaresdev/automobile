namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Get
{
    public class GetMotorcycleResponse
    {
        public string identificador { get; set; } = null!;
        public int ano { get; set; }
        public string modelo { get; set; } = null!;
        public string placa { get; set; } = null!;
    }
}