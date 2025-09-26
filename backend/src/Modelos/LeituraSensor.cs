using System;

namespace MyApp.Modelos
{
    public class LeituraSensor
    {
        public int Id { get; set; }
        public int CorreiaId { get; set; }
        public DateTime DataLeitura { get; set; }
        public double ValorSensor { get; set; } // Ex: valor medido pelo sensor

        // Relação
        public Correia Correia { get; set; }
    }
}
