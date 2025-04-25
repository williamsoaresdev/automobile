using AutomobileRentalManagementAPI.Domain.Entities.Base;

namespace AutomobileRentalManagementAPI.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string Identifier { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
    }
}