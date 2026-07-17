using AppEventos.Domain;
using AppEventos.Persistence.Context;
using AppEventos.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEventos.Persistence.Repositories
{
    internal class LotePersistence(AppEventosContext context) : ILotePersistence
    {
        private readonly AppEventosContext _context = context;

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context
                .Lotes
                .AsNoTracking()
                .Where(l => l.EventoId == eventoId && l.Id == id)
                .OrderByDescending(l => l.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Lote>> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context
                .Lotes
                .AsNoTracking()
                .Where(l => l.EventoId == eventoId)
                .OrderBy(l => l.Id);

            return await query.ToListAsync();
        }
    }
}
