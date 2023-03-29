using AppEventos.Domain;
using AppEventos.Persistence.Context;
using AppEventos.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Persistence.Repositories
{
    public class EventoPersistence : IEventoPersistence
    {

        private readonly AppEventosContext _context;
        
        public EventoPersistence(AppEventosContext context)
        {
            _context = context;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
