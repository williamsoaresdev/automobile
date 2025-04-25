using AutomobileRentalManagementAPI.Domain.Entities.Base;
using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.Domain.Entities
{
    public class DeliveryPerson : BaseEntity
    {
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public CnhType LicenseType { get; set; }
        public string LicenseImageUrl { get; set; } = null!;

        public bool CanCreateLocation() 
        { 
            if(LicenseType == CnhType.A) return true;

            return false;
        }
    }
}