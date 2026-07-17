using AppEventos.Application.DTOs;
using AppEventos.Application.Helpers;
using AppEventos.Application.IServices;
using AppEventos.Domain;
using AppEventos.Persistence.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly ILogger<EventoService> _logger;
        private readonly IBasePersistence _basePersistence;
        private readonly IEventoPersistence _eventoPersistence;

        public EventoService(IBasePersistence basePersistence, IEventoPersistence eventoPersistence, ILogger<EventoService> logger)
        {
            _basePersistence = basePersistence;
            _eventoPersistence = eventoPersistence;
            _logger = logger;
        }

        public async Task<EventoDTO> SaveEvento(EventoDTO model)
        {
            try
            {
                var evento = EventoMapper.ToEntity(model);

                _basePersistence.Save<Evento>(evento);

                if (await _basePersistence.SaveChangesAsync())
                {
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id);

                    return EventoMapper.ToDto(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar evento.");
                throw;
            }
        }

        public async Task<EventoDTO> UpdateEvento(int id, EventoDTO model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                EventoMapper.UpdateEntity(model, evento);

                _basePersistence.Update<Evento>(evento);

                if (await _basePersistence.SaveChangesAsync())
                {
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id);

                    return EventoMapper.ToDto(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar evento de ID: {EventoId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var result = await _eventoPersistence.GetEventoByIdAsync(id, false)
                    ?? throw new Exception("Evento não encontrado!");

                _basePersistence.Delete<Evento>(result);
                return await _basePersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar deletar evento de ID: {EventoId}", id);
                throw;
            }
        }

        public async Task<List<EventoDTO>> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrante);
                if (eventos == null)
                    return null;

                return EventoMapper.ToDTOList(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar todos os eventos.");
                throw;
            }
        }

        public async Task<List<EventoDTO>> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrante);
                if (eventos == null)
                    return null;

                return EventoMapper.ToDTOList(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar eventos por tema: {Tema}", tema);
                throw;
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrante);
                if (evento == null)
                    return null;

                return EventoMapper.ToDto(evento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar evento por ID: {EventoId}", eventoId);
                throw;
            }
        }
    }
}
