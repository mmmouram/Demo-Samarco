using System;

namespace MyApp.Modelos
{
    public class Inspecao
    {
        public int Id { get; set; }
        public int CorreiaId { get; set; }
        public DateTime DataInspecao { get; set; }
        public string Observacao { get; set; }

        // Relação
        public Correia Correia { get; set; }
    }
}
