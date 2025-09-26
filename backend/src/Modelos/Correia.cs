using System;
using System.Collections.Generic;

namespace MyApp.Modelos
{
    public class Correia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; } // e.g.: "Normal", "Aviso", "Crítico"
        public DateTime DataUltimaInspecao { get; set; }

        // Relações
        public ICollection<Inspecao> Inspecoes { get; set; }
        public ICollection<LeituraSensor> LeiturasSensores { get; set; }
    }
}
