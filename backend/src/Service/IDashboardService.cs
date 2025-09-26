using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Modelos;

namespace MyApp.Service
{
    public interface IDashboardService
    {
        Task<IEnumerable<DashboardCorreiaResponse>> ObterDashboardAsync(string status = null, DateTime? dataInspecao = null);
        Task<Correia> ObterDetalhesCorreiaAsync(int id);
    }
}
