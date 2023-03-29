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
        Task<Evento> SaveEvento(Evento evento);
        Task<Evento> UpdateEvento(int id, Evento evento);
        Task<bool> DeleteEvento(int id);

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false );
    }
}
