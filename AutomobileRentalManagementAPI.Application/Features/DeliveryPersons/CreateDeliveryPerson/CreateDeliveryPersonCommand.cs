using AutomobileRentalManagementAPI.Domain.Enums;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson
{
    public class CreateDeliveryPersonCommand : IRequest<CreatedDeliveryPersonResult>
    {
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public CnhType LicenseType { get; set; }
        public string LicenseImageBase64 { get; set; } = null!;
    }
}