using System;

namespace MyApp.Modelos
{
    // DTO para expor dados da correia no dashboard
    public class DashboardCorreiaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Risco { get; set; }
        public bool Alerta { get; set; }
        public DateTime DataUltimaInspecao { get; set; }
    }
}
