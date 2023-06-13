using AppEventos.Application.DTOs;
using AppEventos.Application.IServices;
using AppEventos.Domain;
using AppEventos.Persistence.IRepositories;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public EventoService(IBasePersistence basePersistence, IEventoPersistence eventoPersistence, IMapper mapper)
        {
            _basePersistence = basePersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        public async Task<EventoDTO> SaveEvento(EventoDTO model)
        {
            try 
            {
                var evento = _mapper.Map<Evento>(model);

                _basePersistence.Save<Evento>(evento);

                if (await _basePersistence.SaveChangesAsync())
                {
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDTO>(result);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventoDTO> UpdateEvento(int id, EventoDTO model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _basePersistence.Update<Evento>(evento);

                if (await _basePersistence.SaveChangesAsync())
                {
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDTO>(result);
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

        public async Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrante);
                if(eventos == null) return null;

                var result = _mapper.Map<EventoDTO[]>(eventos);

                return result;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrante);
                if(eventos == null) return null;

                var result = _mapper.Map<EventoDTO[]>(eventos);

                return result;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrante);
                if(evento == null) return null;

                var result = _mapper.Map<EventoDTO>(evento);

                return result;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }        
    }
}
