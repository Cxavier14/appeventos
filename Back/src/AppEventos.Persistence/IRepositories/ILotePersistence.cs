using AppEventos.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppEventos.Persistence.IRepositories
{
    public interface ILotePersistence
    {
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
        Task<List<Lote>> GetLotesByEventoIdAsync(int eventoId);
    }
}
