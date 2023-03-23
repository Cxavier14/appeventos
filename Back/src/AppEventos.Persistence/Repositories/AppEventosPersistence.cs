using AppEventos.Domain;
using AppEventos.Persistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Persistence.Repositories
{
    public class AppEventosPersistence : IAppEventosPersistence
    {
        private readonly AppEventosContext _context;
        public AppEventosPersistence(AppEventosContext context)
        {
            _context = context;
        }

        public void Save<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        //eventos
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

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante)
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

        public Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante)
        {
            throw new NotImplementedException();
        }

        // palestrantes
        public Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            throw new NotImplementedException();
        }
    }
}
