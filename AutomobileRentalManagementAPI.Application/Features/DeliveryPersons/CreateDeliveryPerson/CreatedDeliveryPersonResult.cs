using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson
{
    public class CreatedDeliveryPersonResult
    {
        public Guid NavigationId { get; set; }
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public CnhType LicenseType { get; set; }
        public string LicenseImageUrl { get; set; } = null!;
    }
}