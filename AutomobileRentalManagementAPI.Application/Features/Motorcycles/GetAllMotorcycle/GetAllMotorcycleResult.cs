namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles
{
    public class GetAllMotorcyclesResult
    {
        public List<MotorcycleItem> Items { get; set; } = [];
    }

    public class MotorcycleItem
    {
        public string Identifier { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
    }
}
