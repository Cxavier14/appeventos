using AppEventos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppEventos.Application.IServices
{
    public interface ILoteService
    {
        Task<List<LoteDTO>> SaveLotes(int eventoId, List<LoteDTO> lotes);
        Task<bool> DeleteLote(int eventoId, int id);

        Task<List<LoteDTO>> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDTO> GetLoteByIdsAsync(int eventoId, int id);
    }
}
