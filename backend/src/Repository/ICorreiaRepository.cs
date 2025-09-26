using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Modelos;

namespace MyApp.Repository
{
    public interface ICorreiaRepository
    {
        Task<IEnumerable<Correia>> ObterTodasCorreiasAsync();
        Task<Correia> ObterCorreiaPorIdAsync(int id);
        Task<IEnumerable<Correia>> FiltrarCorreiasAsync(string status, DateTime? dataInspecao);
    }
}
