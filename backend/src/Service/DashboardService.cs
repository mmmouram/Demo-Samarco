using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Modelos;
using MyApp.Repository;

namespace MyApp.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly ICorreiaRepository _correiaRepository;
        private const double LIMIAR_ALERTA = 100.0; // valor de referência para emitir alerta

        public DashboardService(ICorreiaRepository correiaRepository)
        {
            _correiaRepository = correiaRepository;
        }

        public async Task<IEnumerable<DashboardCorreiaResponse>> ObterDashboardAsync(string status = null, DateTime? dataInspecao = null)
        {
            IEnumerable<Models.Correia> correias;

            if (!string.IsNullOrEmpty(status) || dataInspecao.HasValue)
            {
                correias = await _correiaRepository.FiltrarCorreiasAsync(status, dataInspecao);
            }
            else
            {
                correias = await _correiaRepository.ObterTodasCorreiasAsync();
            }

            // Ordenar as correias de acordo com o risco calculado
            var dashboardList = correias
                .Select(c => new DashboardCorreiaResponse
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    DataUltimaInspecao = c.DataUltimaInspecao,
                    Risco = CalcularRisco(c),
                    Alerta = CalcularRisco(c) >= LIMIAR_ALERTA
                })
                .OrderByDescending(d => d.Risco);

            return dashboardList;
        }

        public async Task<Correia> ObterDetalhesCorreiaAsync(int id)
        {
            return await _correiaRepository.ObterCorreiaPorIdAsync(id);
        }

        private double CalcularRisco(Correia correia)
        {
            double risco = 0.0;

            // Considera o maior valor dos sensores se existir
            if (correia.LeiturasSensores != null && correia.LeiturasSensores.Any())
            {
                risco += correia.LeiturasSensores.Max(s => s.ValorSensor);
            }

            // Soma um valor baseado na defasagem da última inspeção
            var dias = (DateTime.Now - correia.DataUltimaInspecao).TotalDays;
            risco += dias / 10.0;

            return risco;
        }
    }
}
