namespace AutomobileRentalManagementAPI.Domain.Entities
{
    public class MotorcycleMongo : Document
    {
        public int Id { get; set; }
        public string NavigationId { get; set; }
        public string Identifier { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
    }
}
