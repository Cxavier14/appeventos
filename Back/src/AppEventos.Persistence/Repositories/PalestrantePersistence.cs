using AppEventos.Domain;
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
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly AppEventosContext _context;

        public PalestrantePersistence(AppEventosContext context)
        {
            _context = context;
        }
                
        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.MiniCurriculo)
                .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Evento);
            }

            query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.MiniCurriculo)
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(e => e.MiniCurriculo)
                .Include(e => e.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query.OrderBy(e => e.Id).Where(e => e.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
