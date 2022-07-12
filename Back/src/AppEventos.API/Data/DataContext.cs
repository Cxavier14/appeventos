using AppEventos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEventos.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Evento> Eventos { get; set; }
    }
}