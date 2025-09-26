using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Config;
using MyApp.Modelos;

namespace MyApp.Repository
{
    public class CorreiaRepository : ICorreiaRepository
    {
        private readonly AppDbContext _context;

        public CorreiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Correia>> ObterTodasCorreiasAsync()
        {
            return await _context.Correias
                .Include(c => c.Inspecoes)
                .Include(c => c.LeiturasSensores)
                .ToListAsync();
        }

        public async Task<Correia> ObterCorreiaPorIdAsync(int id)
        {
            return await _context.Correias
                .Include(c => c.Inspecoes)
                .Include(c => c.LeiturasSensores)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Correia>> FiltrarCorreiasAsync(string status, DateTime? dataInspecao)
        {
            var query = _context.Correias
                .Include(c => c.Inspecoes)
                .Include(c => c.LeiturasSensores)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (dataInspecao.HasValue)
            {
                query = query.Where(c => c.DataUltimaInspecao.Date == dataInspecao.Value.Date);
            }

            return await query.ToListAsync();
        }
    }
}
