using AppEventos.Application.IServices;
using AppEventos.Domain;
using AppEventos.Persistence.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IBasePersistence _basePersistence;
        private readonly IEventoPersistence _eventoPersistence;

        public EventoService(IBasePersistence basePersistence, IEventoPersistence eventoPersistence)
        {
            _basePersistence = basePersistence;
            _eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> SaveEvento(Evento evento)
        {
            try
            {
                _basePersistence.Save<Evento>(evento);
                if(await _basePersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento evento)
        {
            try
            {
                var result = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (result == null) return null;

                evento.Id = result.Id;

                _basePersistence.Update(evento);
                if (await _basePersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var result = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (result == null) throw new Exception("Evento não encontrado!");
                
                _basePersistence.Delete<Evento>(result);
                return await _basePersistence.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }        
    }
}
