namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Get
{
    public class GetLocationResponse
    {
        public string identificador { get; set; } = null!;
        public decimal valor_diaria { get; set; }
        public string entregador_id { get; set; } = null!;
        public string moto_id { get; set; } = null!;
        public DateTime data_inicio { get; set; }
        public DateTime data_termino { get; set; }
        public DateTime data_previsao_termino { get; set; }
        public DateTime data_devolucao { get; set; }
        public decimal valor_total { get; set; }
    }
}