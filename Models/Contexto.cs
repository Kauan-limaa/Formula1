using Microsoft.EntityFrameworkCore;

namespace Formula1.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Piloto>Pilotos { get; set; }
        public DbSet<Equipe>Equipes { get; set; }
        public DbSet<Pista>Pistas { get; set; }
        public DbSet<Temporada>Temporas { get; set; }
        public DbSet<ResultadoCorrida> ResultadoCorridas { get; set; }

        public DbSet<PontuacaoPilotoTemporada> PontuacaoPilotoTemporada { get; set; }

    }
}
