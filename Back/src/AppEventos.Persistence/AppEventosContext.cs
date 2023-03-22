using AppEventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppEventos.Persistence
{
    public class AppEventosContext : DbContext
    {
        public AppEventosContext(DbContextOptions<AppEventosContext> options) 
            : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(pe => new { pe.EventoId, pe.PalestranteId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
    }
}