using Microsoft.EntityFrameworkCore;
using MyApp.Modelos;

namespace MyApp.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Correia> Correias { get; set; }
        public DbSet<Inspecao> Inspecoes { get; set; }
        public DbSet<LeituraSensor> LeiturasSensores { get; set; }
    }
}
