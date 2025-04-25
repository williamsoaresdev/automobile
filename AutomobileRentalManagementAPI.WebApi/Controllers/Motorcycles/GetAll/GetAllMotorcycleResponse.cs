namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.GetAll
{
    public class GetAllMotorcyclesResponse
    {
        public string identificador { get; set; } = null!;
        public int ano { get; set; }
        public string modelo { get; set; } = null!;
        public string placa { get; set; } = null!;
    }
}
