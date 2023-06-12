using AppEventos.Application.DTOs;
using AppEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Application.IServices
{
    public interface IEventoService
    {
        Task<EventoDTO> SaveEvento(EventoDTO evento);
        Task<EventoDTO> UpdateEvento(int id, EventoDTO evento);
        Task<bool> DeleteEvento(int id);

        Task<EventoDTO[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false);
        Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrante = false);
        Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includePalestrante = false );
    }
}
