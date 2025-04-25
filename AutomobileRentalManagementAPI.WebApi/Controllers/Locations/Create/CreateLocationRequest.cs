using AutomobileRentalManagementAPI.Domain.Enums;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Create
{
    public sealed class CreateLocationRequest
    {
        public string entregador_id { get; init; } = null!;
        public string moto_id { get; init; } = null!;
        public DateTime data_inicio { get; init; }
        public DateTime data_termino { get; init; }
        public DateTime data_previsao_termino { get; init; }
        public LocationPlan plano { get; init; }
    }
}